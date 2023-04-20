using System;
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
            gameManagerLoaded?.Invoke();
        }
        
    }
}