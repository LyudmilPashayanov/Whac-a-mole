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
        
        private void Start()
        {
             _uiManager.ShowLoadingScreen(true);
            _playfabManager = new PlayfabManager();
            _gameplayManager = new GameplayManager();
            InitPlayfab();
        }

        private void InitPlayfab()
        {
            _playfabManager.Init(OnPlayfabFinished, OnPlayfabError);
        }
            
        private void OnPlayfabFinished()
        {
            GameManager gameManager = new GameManager(_playfabManager, _uiManager, _gameplayManager);
            gameManager.Init(OnGameManagerInit);
        }

        private void OnGameManagerInit()
        {
            GameBooted();
        }
        
        private void GameBooted()
        {
             _uiManager.ShowLoadingScreen(false);
        }
        
        private void OnPlayfabError()
        {
            _uiManager.ShowPlayfabErrorMessage(true);
        }
    }
}
