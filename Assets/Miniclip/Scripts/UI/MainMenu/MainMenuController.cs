using System;
using Miniclip.Audio;
using UnityEngine;

namespace Miniclip.UI.MainMenu
{
    public class MainMenuController : UIPanel
    {
        [SerializeField] private MainMenuView _view;
        private Action<string> OnNameChosen;
        
        private void Start()
        {
            _view.Subscribe(GoToTutorial,GoToTutorial);
        }

        public void Subscribe(Action<string> onNameChosen)
        {
            OnNameChosen = onNameChosen;
        }

        private void GoToTutorial()
        {
            OnNameChosen.Invoke(_view.GetName());
            AudioManager.Instance.PlayButtonClickSound();
            Owner.SwitchPanel(Panel.Tutorial);
        }
        
        private void GoToGame()
        {
            OnNameChosen.Invoke(_view.GetName());
            AudioManager.Instance.PlayButtonClickSound();
            Owner.SwitchPanel(Panel.Gameplay);
        }

        public override void ShowPanel(bool playAnimation = true)
        {
            _view.EnableTextInput(true);
            _view.StartTitleAnimation(true);
            base.ShowPanel(playAnimation);
        }

        protected override void OnViewLeft()
        {
            _view.Reset();
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
