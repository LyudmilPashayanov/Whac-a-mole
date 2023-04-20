using System;
using Miniclip.UI.MainMenu;
using Miniclip.UI.Tutorial;
using UnityEngine;
using UnityEngine.Serialization;

namespace Miniclip.UI
{
    public enum Panel
    {
        MainMenu,
        Tutorial,
        Gameplay,
        Highscores
    }
    
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private RectTransform playfabErrorPanel;
        [SerializeField] private MainMenuController _mainMenuController;
        [SerializeField] private TutorialController _tutorialController;
        private UIPanel _activePanel;
        
        private void Awake()
        {
            _mainMenuController.Init(this);
            _tutorialController.Init(this);

            _activePanel = _mainMenuController;
            SwitchPanel(Panel.MainMenu);
        }

        public void SwitchPanel(Panel panel)
        {
            switch (panel)
            {
                case Panel.MainMenu:
                    if (_activePanel != null)
                    {
                        _activePanel.OnDismissComplete += ShowMainMenu;
                        break;
                    }
                    ShowMainMenu();
                    break;
                case Panel.Tutorial:
                    if (_activePanel != null)
                    {
                        _activePanel.OnDismissComplete += ShowTutorial; 
                        break;
                    }
                    ShowTutorial();
                    break;
                case Panel.Gameplay:
                    if (_activePanel != null)
                    {
                        _activePanel.OnDismissComplete += ShowGameplay;
                        break;
                    }
                    ShowGameplay();
                    break;
                case Panel.Highscores:
                    if (_activePanel != null)
                    {
                        _activePanel.OnDismissComplete += ShowHighScores;
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
            _mainMenuController.ShowPanel();
            _activePanel.OnDismissComplete -= ShowMainMenu;
            _activePanel = _mainMenuController;
        }
        
        private void ShowTutorial()
        {
            _tutorialController.ShowPanel();
            _activePanel.OnDismissComplete -= ShowTutorial;
            _activePanel = _tutorialController;
        }
        
        private void ShowGameplay()
        {
            //_gameplayController.ShowPanel();
            _activePanel.OnDismissComplete -= ShowGameplay;
           // _activePanel = _gameplayController;
        }
        
        private void ShowHighScores()
        {
            //_highScoreController.ShowPanel();
            _activePanel.OnDismissComplete -= ShowHighScores;
            //_activePanel = _highScoreController;
        }
        
        public void ShowLoadingScreen(bool enable)
        {
            
        }

        public void ShowPlayfabErrorMessage(bool enable)
        {
            
        }
    }
}