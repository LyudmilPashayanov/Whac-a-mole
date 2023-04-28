using System;
using System.Collections.Generic;
using Miniclip.Entities;
using Miniclip.Game.Gameplay;
using Miniclip.Playfab;
using Miniclip.UI;
using UnityEngine;
using UnityEngine.U2D;

namespace Miniclip.Game
{
    
    /// <summary>
    /// This class serves as a bridge from the <see cref="GameplayManager"/> and the <see cref="UIManager"/>.
    /// All the logic for the game which requires the Unity Engine happens in this class.
    /// Handles game logic related to time, score, and game over conditions
    /// Handles player input and updates the score accordingly
    /// Handles game over and high score management
    /// Handles game restart
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        #region Variables

        [SerializeField] private UIManager _uiManager;
        [SerializeField] private MoleController _molePrefab;
        [SerializeField] private SpriteAtlas _molesAtlas;
        
        private PlayfabManager _playfabManager;
        private GameplayManager _gameplayManager;
        private ScoringManager _scoringManager;
        private PlayerData _playerAttemptData;
        private List<MoleController> _shownMoles = new List<MoleController>();
        
        private string _playerName;

        private float _timer = 0f;
        private float _timeBetweenMoles = 1f; // Delay in seconds
        
        private bool _spawningMoles = false;
        private bool _gameOn;

        #endregion

        #region Functionality

          private void Update()
        {
            if (_spawningMoles)
            {
                _timer += Time.deltaTime;

                if (_timer >= _timeBetweenMoles)
                {
                    _timer = 0f;
                    _timeBetweenMoles = _gameplayManager.GetTimeBetweenMoles();
                    SpawnMole();
                }
            }
        }
        
        public void Init(PlayfabManager playfabManager, Action gameManagerLoaded)
        {
            _playfabManager = playfabManager;
            _playerAttemptData = playfabManager.PlayerAttemptData;
            
            _uiManager.MainMenuController.Subscribe(SetName);
            _uiManager.GameplayController.OnShowComplete += StartWhacAMole;
            _uiManager.GameplayController.Subscribe(OnUnpauseGame, OnGameLeft, PauseGame);

            MoleFactory moleFactory = new MoleFactory(_molePrefab.gameObject,_molesAtlas);
            _gameplayManager = new GameplayManager(moleFactory);
            _scoringManager = new ScoringManager(playfabManager.GameData, UpdateScores);
            
            gameManagerLoaded?.Invoke();
        }
                
        private void SetName(string name)
        {
            _playerName = name;
        }
        
        private void StartWhacAMole()
        {
            _scoringManager.ResetManager();
            _uiManager.GameplayController.EnablePauseButton(true);
            _uiManager.GameplayController.ShowStartingTimer(OnStartAnimationFinished);
            void OnStartAnimationFinished()
            {
                _uiManager.GameplayController.StartTimerCountdown(_playfabManager.GameData.Timer,GameFinished);
                _timeBetweenMoles = _gameplayManager.GetTimeBetweenMoles();
                _gameOn=true;
                StartSpawning();
            }
        }

        private void UpdateScores(ScoreData scoreData)
        {
            _uiManager.GameplayController.UpdateScore(scoreData);
        }

        private void StartSpawning()
        {
            _spawningMoles = true;
            for (int i = 0; i < _shownMoles.Count; i++)
            {
                _shownMoles[i].ToggleInteractable(true);
            }
        }

        private void SpawnMole()
        {
            MoleController spawnedMole = _gameplayManager.SpawnMole(_gameplayManager.GetRandomMoleType());
            _shownMoles.Add(spawnedMole);
            spawnedMole.SubscribeOnDieEvent(_scoringManager.CalculateScoring);
            spawnedMole.SubscribeOnDespawnEvent(OnMoleDespawned);
            PositionSpawnedMole(spawnedMole);
            spawnedMole.ShowMole(_gameplayManager.GetMoleAliveTime());
        }
        
        private void PositionSpawnedMole(MoleController spawnedMole)
        {
            spawnedMole.SetupPosition(GetAvailableSpawningPosition());
        }

        private RectTransform GetAvailableSpawningPosition()
        {
            int spawningPositionIndex = _gameplayManager.GetRandomPosition();
            return _uiManager.GameplayController.GetSpawningPosition(spawningPositionIndex);
        }

        private void OnMoleDespawned(MoleController despawnedMole)
        {
            int freedIndex = _uiManager.GameplayController.GetSpawningPointIndex(despawnedMole.SpawningPoint);
            _gameplayManager.FreeSpawnPosition(freedIndex);
            RemoveShownMole(despawnedMole);
            despawnedMole.UnsubscribeOnDespawnEvent(OnMoleDespawned);
        }

        private void StopSpawning()
        {
            _spawningMoles = false;
            for (int i = 0; i < _shownMoles.Count; i++)
            {
                _shownMoles[i].ToggleInteractable(false);
            }
        }

        private void RemoveShownMole(MoleController mole)
        {
            _shownMoles.Remove(mole);
        }

        #region Pause Logic

        private void PauseGame()
        {
            _uiManager.GameplayController.Pause();
            _uiManager.GameplayController.StopTimerCountdown();
            for (int i = 0; i < _shownMoles.Count; i++)
            {
                _shownMoles[i].PauseMole();
            }
            StopSpawning();
        }
        
        private void OnUnpauseGame()
        {
            _uiManager.GameplayController.HidePauseMenu();
            if (_gameOn)
            {
                _uiManager.GameplayController.ResumeTimerCountdown();
                for (int i = 0; i < _shownMoles.Count; i++)
                {
                    _shownMoles[i].UnpauseMole();
                }
                StartSpawning();
            }
            
        }

        #endregion

        #region Stopping Gameplay Logic

        
        private void OnGameLeft()
        {
            ResetField();
            _uiManager.SwitchPanel(Panel.MainMenu);
        }
        
        private void GameFinished()
        {
            ResetField();
            StopSpawning();
            SaveProgress();
            _uiManager.GameplayController.FinishGame(() =>
            {
                _uiManager.SwitchPanel(Panel.HighScores);
            });
        }
        
        private void ResetField()
        {
            _gameOn = false;
            _gameplayManager.ResetManager();
            List<MoleController> shallowCopy = _shownMoles.GetRange(0, _shownMoles.Count);
            for (int i = 0; i < shallowCopy.Count; i++)
            {
                shallowCopy[i].ResetMole();
            }
        }
        
        private void SaveProgress()
        {
            AttemptData newAttempt = new AttemptData();
            newAttempt.Name = _playerName;
            newAttempt.Score = _scoringManager.GetOverallHits(); 
            
            _playerAttemptData.AddAttempt(newAttempt);
            _playfabManager.SavePlayerAttempts(_playerAttemptData);
            
            if(_playerAttemptData.IsAttemptRecord(newAttempt))
                _playfabManager.UpdateLeaderboard(newAttempt);
        }
        
        #endregion

        #endregion
      
    }
}