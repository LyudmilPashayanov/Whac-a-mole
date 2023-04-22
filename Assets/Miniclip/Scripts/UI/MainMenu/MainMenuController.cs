using Miniclip.Audio;
using UnityEngine;

namespace Miniclip.UI.MainMenu
{
    public class MainMenuController : UIPanel
    {
        [SerializeField] private MainMenuView _view;

        private void Start()
        {
            _view.Subscribe(GoToTutorial,GoToTutorial);
        }

        private void GoToTutorial()
        {
            AudioManager.Instance.PlayButtonClickSound();
            Owner.SwitchPanel(Panel.Tutorial);
        }
        
        private void GoToGame()
        {
            AudioManager.Instance.PlayButtonClickSound();
            Owner.SwitchPanel(Panel.Gameplay);
        }

        public override void ShowPanel(bool playAnimation = true)
        {
            _view.EnableTextInput(true);
            _view.StartTitleAnimation(true);
            base.ShowPanel(playAnimation);
        }

        public override void HidePanel(bool playAnimation = true)
        {
            _view.EnableTextInput(false);
            _view.SetStartGameButtonInteractable(false);
            _view.SetTutorialButtonInteractable(false);
            _view.StartTitleAnimation(false);
            base.HidePanel(playAnimation);
        }
    }
}
