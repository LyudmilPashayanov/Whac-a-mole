using System;
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

        public void Init(PlayfabManager playfabManager, Action gameManagerLoaded)
        {
            _playfabManager = playfabManager;
            MoleFactory moleFactory = new MoleFactory(_molePrefab.gameObject,_molesAtlas);
            _gameplayManager = new GameplayManager(moleFactory);
            _scoringManager = new ScoringManager(playfabManager.GameData, UpdateScores);
            _uiManager.GameplayController.Subscribe(OnUnpauseGame, OnGameLeft, PauseGame);
            gameManagerLoaded?.Invoke();
        }
        
        public void StartWhacAMole()
        {
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
            // Position the Mole correctly and make it start appearing
            spawnedMole.SubscribeOnDiedEvent(_scoringManager.CalculateScoring);
        }

        private void GameFinished()
        {
            // save the results to playfab
            // update the current saved data
            // reset and destroy everything that has to be created
            _uiManager.GameplayController.FinishGame(() =>
            {
                _uiManager.SwitchPanel(Panel.HighScores);
            });
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