using System;
using Miniclip.Audio;
using Miniclip.Entities;
using UnityEngine;

namespace Miniclip.UI.MainMenu
{
    public class MainMenuController : UIPanel
    {
        [SerializeField] private MainMenuView _view;
        private Action<string> OnNameChosen;
        private PlayerOptionsData _playerOptionsData;
        
        private void Start()
        {
            _view.Subscribe(GoToTutorial, CheckButtonActivations);
        }

        public void Subscribe(Action<string> onNameChosen)
        {
            OnNameChosen = onNameChosen;
        }

        private void CheckButtonActivations(string text)
        {
            if (text.Length > 2)
            {
                _view.SetMainGameButtonInteractable(true);
                if (_playerOptionsData.ShowTutorial == false)
                {
                    _view.SetTutorialButtonInteractable(true);
                    _view.ShowTutorialButton(true);
                }
            }
            else
            {
                _view.SetTutorialButtonInteractable(false);
                _view.ShowTutorialButton(false);
                _view.SetMainGameButtonInteractable(false);
            }
        }
        
        private void GoToTutorial()
        {
            Debug.Log("GoToTutorial GoToTutorial GoToTutorialGoToTutorial");
            OnNameChosen.Invoke(_view.GetName());
            AudioManager.Instance.PlayButtonClickSound();
            Owner.SwitchPanel(Panel.Tutorial);
        }
        
        private void GoToGame()
        {
            Debug.Log("GoToGame GoToGame GoToGame GoToGame");
            OnNameChosen.Invoke(_view.GetName());
            AudioManager.Instance.PlayButtonClickSound();
            Owner.SwitchPanel(Panel.Gameplay);
        }

        public override void ShowPanel(bool playAnimation = true)
        {
            _view.EnableTextInput(true);
            _view.StartTitleAnimation(true);
            if (_playerOptionsData.ShowTutorial)
            {
                _view.SetMainButtonSettings("Tutorial", GoToTutorial);
            }
            else
            {
                _view.SetMainButtonSettings("Start Game", GoToGame);
            }
            base.ShowPanel(playAnimation);
        }

        protected override void OnViewLeft()
        {
            _view.Reset();
        }

        public override void HidePanel(bool playAnimation = true)
        {
            _view.EnableTextInput(false);
            _view.SetMainGameButtonInteractable(false);
            _view.SetTutorialButtonInteractable(false);
            _view.StartTitleAnimation(false);
            base.HidePanel(playAnimation);
        }

        public void Init(PlayerOptionsData playerOptionsData)
        {
            _playerOptionsData = playerOptionsData;
        }
    }
}
