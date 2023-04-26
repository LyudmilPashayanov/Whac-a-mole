using System;
using Miniclip.Audio;
using Miniclip.Entities;
using UnityEngine;

namespace Miniclip.UI.MainMenu
{
    /// <summary>
    /// This class is responsible for the Main Menu Panel in Whac-A-Mole.
    /// All the logic for the Main Menu is handled here.
    /// </summary>
    public class MainMenuController : UIPanel
    {
        #region Variables

        [SerializeField] private MainMenuView _view;
        private Action<string> OnNameChosen;
        private PlayerOptionsData _playerOptionsData;

        #endregion

        #region Functionality

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

        #endregion

        #region EventHandlers

        protected override void OnViewLeft()
        {
            _view.Reset();
        }

        #endregion
    }
}
