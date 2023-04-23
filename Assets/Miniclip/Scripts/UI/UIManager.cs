using System;
using DG.Tweening;
using Miniclip.Game;
using Miniclip.UI.Gameplay;
using Miniclip.UI.HighScore;
using Miniclip.UI.MainMenu;
using Miniclip.UI.Tutorial;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
using Sequence = DG.Tweening.Sequence;

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
        [SerializeField] private GameObject _infoTab;
        [SerializeField] private TMP_Text _infoTabText;
        [SerializeField] public MainMenuController _mainMenuController;
        [SerializeField] public TutorialController _tutorialController;
        [SerializeField] public HighScoreController _highScoreController;
        [SerializeField] public GameplayController _gameplayController;
        
        private UIPanel _activePanel;
        private GameManager _gameManager;
        private Sequence _loadingScreenTextSequence;
        private string[] _niceArray = new[] { "You are the awesome!","Keep it up, you are doing great!","Yeyyy, you chose to play my game!" };
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
            _gameplayController.OnPresentComplete += _gameManager.StartWhacAMole;
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
            if (enable)
            {
                _loadingScreenTextSequence = DOTween.Sequence(); // using do tween timing as a Coroutine.
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
    }
}