using Miniclip.Game;
using Miniclip.Game.Gameplay;
using Miniclip.Playfab;
using Miniclip.UI;
using UnityEngine;

namespace Miniclip
{
    public class BootManager : MonoBehaviour
    {
        [SerializeField] private UIManager _uiManager;
        
        private PlayfabManager _playfabManager;
        private GameplayManager _gameplayManager;
        private GameManager _gameManager;
        private void Start()
        {
             _uiManager.ShowLoadingScreen(true);
            _playfabManager = new PlayfabManager();
            _gameplayManager = new GameplayManager();
            InitPlayfab();
        }

        private void InitPlayfab()
        {
            _playfabManager.Init(OnPlayfabInitiated, OnPlayfabError);
        }
            
        private void OnPlayfabInitiated()
        {
            _gameManager = new GameManager(_playfabManager, _uiManager, _gameplayManager);
            _uiManager.Init(_gameManager);
            _gameManager.Init(OnGameManagerInitiated);
        }

        private void OnGameManagerInitiated()
        {
            GameBooted();
        }
        
        private void GameBooted()
        {
             _uiManager.ShowLoadingScreen(false);
             _gameManager.GameBooted();
        }
        
        private void OnPlayfabError()
        {
            _uiManager.ShowPlayfabErrorMessage(true);
        }
    }
}
