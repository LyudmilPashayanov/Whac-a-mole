using System;
using Miniclip.Entities;
using Miniclip.Game.Gameplay;
using Miniclip.Playfab;
using Miniclip.UI;

namespace Miniclip.Game
{
    public class GameManager
    {
        private PlayfabManager _playfabManager;
        private UIManager _uiManager;
        private GameplayManager _gameplayManager;
        private GameData _gameData; 
        
        public GameManager(PlayfabManager playfabManager, UIManager uiManager, GameplayManager gameplayManager)
        {
            _playfabManager = playfabManager;
            _uiManager = uiManager;
            _gameplayManager = gameplayManager;
        }

        public void Init(Action gameManagerLoaded)
        {
            _gameData = _playfabManager.GameData; 
            _gameplayManager.Init(_gameData);
            gameManagerLoaded?.Invoke();
        }

        public void GameBooted()
        {
            _uiManager.SwitchPanel(Panel.MainMenu);
        }

        public void PrepareWhacAMole()
        {
            _uiManager._gameplayController.ShowStartingTimer(3,StartWhacAMole);
        }

        private void StartWhacAMole()
        {
            _gameplayManager.GetRandomMole();
            _uiManager._gameplayController.StartTimerCountdown(_gameData.Timer,GameFinished);
        }

        private void GameFinished()
        {
            // save the results to playfab
            // update the current saved data
            // reset and destroy everything that has to be destroyed
            _uiManager.SwitchPanel(Panel.Highscores);
        }
    }
}