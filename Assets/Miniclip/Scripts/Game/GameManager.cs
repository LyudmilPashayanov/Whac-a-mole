using System;
using Miniclip.Entities;
using Miniclip.Game.Gameplay;
using Miniclip.Playfab;
using Miniclip.UI;
using UnityEngine;
using UnityEngine.U2D;

namespace Miniclip.Game
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private UIManager _uiManager;
        [SerializeField] private MoleController _molePrefab;
        [SerializeField] private SpriteAtlas _molesAtlas;
        
        private PlayfabManager _playfabManager;
        private GameplayManager _gameplayManager;
        private GameData _gameData;

        public void Init(PlayfabManager playfabManager, Action gameManagerLoaded)
        {
            _playfabManager = playfabManager;
            MoleFactory moleFactory = new MoleFactory(_molePrefab.gameObject,_molesAtlas);
            _gameplayManager = new GameplayManager(_playfabManager.GameData, moleFactory);
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
            //_gameplayManager.GetRandomMole();
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