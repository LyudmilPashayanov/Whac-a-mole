using System;
using Miniclip.Game;
using Miniclip.UI.Gameplay;
using Miniclip.UI.HighScore;
using Miniclip.UI.MainMenu;
using Miniclip.UI.Tutorial;
using Unity.VisualScripting;
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
        [SerializeField] public MainMenuController _mainMenuController;
        [SerializeField] public TutorialController _tutorialController;
        [SerializeField] public HighScoreController _highScoreController;
        [SerializeField] public GameplayController _gameplayController;
        
        private UIPanel _activePanel;
        private GameManager _gameManager;
        
        public void Init(GameManager gameManager)
        {
            _gameManager = gameManager;
            _mainMenuController.Init(this);
            _tutorialController.Init(this);
            _highScoreController.Init(this);
            _gameplayController.Init(this);
            
            _activePanel = _mainMenuController;
        }
        
        public void SwitchPanel(Panel panel)  // TODO: Check if you can remove the checks for the activePanel as they are always NOT null.
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
            _gameplayController.OnPresentComplete += _gameManager.PrepareWhacAMole;
            _gameplayController.ShowPanel();
            
            _activePanel.OnDismissComplete -= ShowGameplay;
            _activePanel = _gameplayController;
        }
        
        private void ShowHighScores()
        {
            _highScoreController.ShowPanel();
            _activePanel.OnDismissComplete -= ShowHighScores;
            _activePanel = _highScoreController;
        }
        
        public void ShowLoadingScreen(bool enable)
        {
            
        }

        public void ShowPlayfabErrorMessage(bool enable)
        {
            
        }
    }
}