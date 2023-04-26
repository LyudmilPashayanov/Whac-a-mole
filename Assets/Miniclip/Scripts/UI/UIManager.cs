using System;
using DG.Tweening;
using Miniclip.UI.Gameplay;
using Miniclip.UI.HighScore;
using Miniclip.UI.MainMenu;
using Miniclip.UI.Tutorial;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Miniclip.UI
{
    /// <summary>
    /// All UI panels in Whac-A-Mole
    /// </summary>
    public enum Panel
    {
        MainMenu,
        Tutorial,
        Gameplay,
        HighScores
    }
    
    /// <summary>
    /// This class serves as an orchestrator for the UI in the game.
    /// Here you can access all the different UI controllers
    /// and Navigate from one UI view to another 
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        #region Variables
        
        private readonly string[] _niceArray = new[] { "You are awesome!", "You are the best!", "Keep it up, you are doing great!", "Eyyy, you chose to play my game!" };

        [SerializeField] private GameObject _infoTab;
        [SerializeField] private TMP_Text _infoTabText;
        
        [SerializeField] public MainMenuController MainMenuController;
        [SerializeField] public TutorialController TutorialController;
        [SerializeField] public HighScoreController HighScoreController;
        [SerializeField] public GameplayController GameplayController;
        
        private UIPanel _activePanel;
        private Sequence _loadingScreenTextSequence;

        #endregion

        #region Functionality

        
     public void Init()
        {
            MainMenuController.Subscribe(this);
            TutorialController.Subscribe(this);
            HighScoreController.Subscribe(this);
            GameplayController.Subscribe(this);
            
            _activePanel = MainMenuController;
        }
        
        /// <summary>
        /// Navigates to a different UI Panel.
        /// </summary>
        /// <param name="panel">The panel you want to navigate to.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void SwitchPanel(Panel panel)
        {
            switch (panel)
            {
                case Panel.MainMenu:
                    if (_activePanel != null)
                    {
                        _activePanel.OnHideComplete += ShowMainMenu;
                        break;
                    }
                    ShowMainMenu();
                    break;
                case Panel.Tutorial:
                    if (_activePanel != null)
                    {
                        _activePanel.OnHideComplete += ShowTutorial; 
                        break;
                    }
                    ShowTutorial();
                    break;
                case Panel.Gameplay:
                    if (_activePanel != null)
                    {
                        _activePanel.OnHideComplete += ShowGameplay;
                        break;
                    }
                    ShowGameplay();
                    break;
                case Panel.HighScores:
                    if (_activePanel != null)
                    {
                        _activePanel.OnHideComplete += ShowHighScores;
                        break;
                    }
                    ShowHighScores();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(panel), panel, null);
            }

            if (_activePanel != null)
            {
                _activePanel.HidePanel();
            }
        }

        private void ShowMainMenu()
        {
            MainMenuController.ShowPanel();
            _activePanel.OnHideComplete -= ShowMainMenu;
            _activePanel = MainMenuController;
        }
        
        private void ShowTutorial()
        {
            TutorialController.ShowPanel();
            _activePanel.OnHideComplete -= ShowTutorial;
            _activePanel = TutorialController;
        }
        
        private void ShowGameplay()
        {
            GameplayController.ShowPanel();
            _activePanel.OnHideComplete -= ShowGameplay;
            _activePanel = GameplayController;
        }
        
        private void ShowHighScores()
        {
            HighScoreController.ShowPanel();
            _activePanel.OnHideComplete -= ShowHighScores;
            _activePanel = HighScoreController;
            HighScoreController.SetupBoard();
        }
        
        /// <summary>
        /// Shows a loading screen and gives you nice positive energy if you have to wait :)
        /// </summary>
        /// <param name="enable"></param>
        public void ShowLoadingScreen(bool enable)
        {
            if (enable)
            {
                _infoTabText.text = "Connecting to server ...";
                _loadingScreenTextSequence = DOTween.Sequence(); // using To tween timing as a Coroutine.
                _loadingScreenTextSequence.Append(DOTween.To(value => value = 0, 0, 1, 2f).OnComplete(() =>
                {
                    _infoTabText.text = "Connecting to server .";
                }));
                for (int i = 0; i < 2; i++)
                {
                    _loadingScreenTextSequence.Append(DOTween.To(value => value = 0, 0, 1, 0.8f).OnComplete(() =>
                    {
                        _infoTabText.text += ".";
                    }));
                }
                _loadingScreenTextSequence.Append(DOTween.To(value => value = 0, 0, 1, 0.8f).OnComplete(() =>
                {
                    _infoTabText.text = _niceArray[Random.Range(0,_niceArray.Length)]; // Maybe use Tetris Random here
                }));
                _loadingScreenTextSequence.SetLoops(-1);
                _infoTab.SetActive(true);
            }
            else
            {
                _loadingScreenTextSequence.Kill();
                _infoTab.SetActive(false);
            }
        }

        public void ShowPlayfabErrorMessage()
        {
            ShowLoadingScreen(false);
            _infoTabText.text = "There has been a problem with the server.\nPlease restart the game.";
            _infoTab.SetActive(true);
        }
        
        #endregion

        #region EventHandlers

        #endregion
   
    }
}