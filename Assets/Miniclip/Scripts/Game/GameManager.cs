using System;
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
        
        public void Init(PlayfabManager playfabManager, Action gameManagerLoaded)
        {
            _playfabManager = playfabManager;
            _playerAttemptData = playfabManager.PlayerAttemptData;
            _uiManager.MainMenuController.Subscribe((name)=> _playerName = name);
            MoleFactory moleFactory = new MoleFactory(_molePrefab.gameObject,_molesAtlas);
            _gameplayManager = new GameplayManager(moleFactory);
            _scoringManager = new ScoringManager(playfabManager.GameData, UpdateScores);
            _uiManager.GameplayController.Subscribe(OnUnpauseGame, OnGameLeft, PauseGame);
            gameManagerLoaded?.Invoke();
        }
        
        public void StartWhacAMole()
        {
            _scoringManager.ResetManager();
            _uiManager.GameplayController.ShowStartingTimer(OnStartAnimationFinished);
            void OnStartAnimationFinished()
            {
                _uiManager.GameplayController.StartTimerCountdown(_playfabManager.GameData.Timer,GameFinished);
                StartSpawning();
            }
        }

        private void UpdateScores(ScoreData scoreData)
        {
            _uiManager.GameplayController.UpdateScore(scoreData);
        }
        
        private void StartSpawning()
        {
            MoleController spawnedMole = _gameplayManager.GetRandomMole(); // TODO: Specify what type of randoms do you want
            spawnedMole.SubscribeOnDieEvent(_scoringManager.CalculateScoring);
            spawnedMole.SubscribeOnDespawnEvent(ReturnAvailablePosition);
            PositionSpawnedMole(spawnedMole);
            
            spawnedMole.ShowMole();
            
            //  spawnedMole.Wait a bit
            //    SpawnedMole start hiding
            // start spawning next, depending on the game rules.
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

        private void ReturnAvailablePosition(RectTransform freeSpawnPoint)
        {
            int freedIndex = _uiManager.GameplayController.GetSpawningPointIndex(freeSpawnPoint);
            _gameplayManager.FreeSpawnPosition(freedIndex);
        }
        
        private void GameFinished()
        {
            // save the results to playfab
            // update the current saved data
            // reset and destroy everything that has to be created
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