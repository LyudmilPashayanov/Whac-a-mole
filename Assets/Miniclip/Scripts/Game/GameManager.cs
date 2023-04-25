using System;
using System.Collections;
using Miniclip.Entities;
using Miniclip.Game.Gameplay;
using Miniclip.Playfab;
using Miniclip.UI;
using UnityEngine;
using UnityEngine.U2D;

namespace Miniclip.Game
{
    
    /*
    Instantiates the GameplayManager and UIManager
    Handles game logic related to time, score, and game over conditions
    Handles player input and updates the score accordingly    
    Handles game over and high score management
    Handles game restart
    */
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private UIManager _uiManager;
        [SerializeField] private MoleController _molePrefab;
        [SerializeField] private SpriteAtlas _molesAtlas;
        
        private PlayfabManager _playfabManager;
        private GameplayManager _gameplayManager;
        private ScoringManager _scoringManager;
        private PlayerData _playerAttemptData;
        private string _playerName;
        private bool _spawnMoles;
        
        public void Init(PlayfabManager playfabManager, Action gameManagerLoaded)
        {
            _playfabManager = playfabManager;
            _playerAttemptData = playfabManager.PlayerAttemptData;

            _uiManager.MainMenuController.Subscribe((name)=> _playerName = name);
            _uiManager.GameplayController.Subscribe(OnUnpauseGame, OnGameLeft, PauseGame);

            MoleFactory moleFactory = new MoleFactory(_molePrefab.gameObject,_molesAtlas);
            _gameplayManager = new GameplayManager(moleFactory);
            _scoringManager = new ScoringManager(playfabManager.GameData, UpdateScores);
            
            gameManagerLoaded?.Invoke();
        }
        
        public void StartWhacAMole()
        {
            _scoringManager.ResetManager();
            _uiManager.GameplayController.ShowStartingTimer(OnStartAnimationFinished);
            void OnStartAnimationFinished()
            {
                StartSpawning();
            }
        }

        private void UpdateScores(ScoreData scoreData)
        {
            _uiManager.GameplayController.UpdateScore(scoreData);
        }
        
        private void StartSpawning()
        {
            _spawnMoles = true;
            _uiManager.GameplayController.StartTimerCountdown(_playfabManager.GameData.Timer,GameFinished);
            StartCoroutine(SpawnMole());
        }

        private IEnumerator SpawnMole()
        {
            while (_spawnMoles)
            {
                MoleController spawnedMole = _gameplayManager.SpawnMole(_gameplayManager.GetRandomMoleType());
                spawnedMole.SubscribeOnDieEvent(_scoringManager.CalculateScoring);
                spawnedMole.SubscribeOnDespawnEvent(ReturnAvailablePosition);
                PositionSpawnedMole(spawnedMole);
                spawnedMole.ShowMole(_gameplayManager.GetMoleAliveTime());
                
                yield return new WaitForSeconds(_gameplayManager.GetTimeBetweenMoles());
            }
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

        private void ReturnAvailablePosition(MoleController despawnedMole)
        {
            int freedIndex = _uiManager.GameplayController.GetSpawningPointIndex(despawnedMole.SpawningPoint);
            _gameplayManager.FreeSpawnPosition(freedIndex);
        }
        
        private void GameFinished()
        {
            // TODO: reset and destroy everything that has to be created
            _spawnMoles = false;
            SaveProgress();
            _uiManager.GameplayController.FinishGame(() =>
            {
                _uiManager.SwitchPanel(Panel.HighScores);
            });
        }

        private void SaveProgress()
        {
            AttemptData newAttempt = new AttemptData();
            newAttempt.Name = _playerName;
            newAttempt.Score = _scoringManager.GetOverallHits(); 
            
            _playerAttemptData.AddAttempt(newAttempt);
            _playfabManager.SavePlayerAttempts(_playerAttemptData);
        }
        private void PauseGame()
        {
            _uiManager.GameplayController.Pause();
            _uiManager.GameplayController.StopTimerCountdown();
        }
        
        private void OnUnpauseGame()
        {
            _uiManager.GameplayController.HidePauseMenu();
            _uiManager.GameplayController.ResumeTimerCountdown();
        }

        private void OnGameLeft()
        {
            //destroy and clean all spawned stuff
            _uiManager.SwitchPanel(Panel.MainMenu);
        }
    }
}