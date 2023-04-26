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
        [SerializeField] private GameManager _gameManager;

        private PlayfabManager _playfabManager;
        private void Start()
        {
             _uiManager.ShowLoadingScreen(true);
            _playfabManager = new PlayfabManager();
            InitPlayfab();
        }

        private void InitPlayfab()
        {
            _playfabManager.Init(OnPlayfabInitiated, OnPlayfabError);
        }
            
        private void OnPlayfabInitiated()
        {
            _uiManager.Init(_gameManager);
            _uiManager.HighScoreController.Init(_playfabManager.PlayerAttemptData);
            _gameManager.Init(_playfabManager, OnGameManagerInitiated);
        }

        private void OnGameManagerInitiated()
        {
            GameBooted();
        }
        
        private void GameBooted()
        {
             _uiManager.ShowLoadingScreen(false);
             _uiManager.SwitchPanel(Panel.MainMenu);
        }
        
        private void OnPlayfabError()
        {
            _uiManager.ShowPlayfabErrorMessage();
        }
    }
}
