using Miniclip.Game;
using Miniclip.Playfab;
using Miniclip.UI;
using UnityEngine;

namespace Miniclip
{
    /// <summary>
    /// This class is the first thing that runs in the game.
    /// It is responsible to create the classes in the needed order
    /// and if needed inject them with one another, so that they can
    /// hold the references they need.
    /// </summary>
    public class BootManager : MonoBehaviour
    {
        #region Variables

        [SerializeField] private UIManager _uiManager;
        [SerializeField] private GameManager _gameManager;

        private PlayfabManager _playfabManager;

        #endregion

        #region Functionality

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
            _uiManager.Init();
            _uiManager.HighScoreController.Init(_playfabManager.PlayerAttemptData, _playfabManager.GetLeaderboard);
            _uiManager.MainMenuController.Init(_playfabManager.PlayerOptionsData);
            _uiManager.TutorialController.Init(_playfabManager.PlayerOptionsData, _playfabManager.SavePlayerOptions);
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

        #endregion
       
    }
}
