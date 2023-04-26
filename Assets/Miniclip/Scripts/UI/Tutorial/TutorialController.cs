using System;
using Miniclip.Audio;
using Miniclip.Entities;
using UnityEngine;

namespace Miniclip.UI.Tutorial
{
    public class TutorialController : UIPanel
    {
        [SerializeField] private TutorialView _view;

        private event Action<PlayerOptionsData> OnGameStarted;
        private PlayerOptionsData _playerOptionsData;
        
        private void Start()
        {
            _view.Subscribe(StartGameplay);
        }
        
        public void Init(PlayerOptionsData playerOptionsData, Action<PlayerOptionsData> savePlayerOptions)
        {
            OnGameStarted += savePlayerOptions;
            _playerOptionsData = playerOptionsData;
        }
        
        private void StartGameplay()
        {
            AudioManager.Instance.PlayButtonClickSound();
            _playerOptionsData.ShowTutorial = GetTutorialAgain();
            OnGameStarted?.Invoke(_playerOptionsData);
            Owner.SwitchPanel(Panel.Gameplay);
        }
        
        private bool GetTutorialAgain()
        {
            return _view.GetTutorialAgainCheck();
        }

        public override void ShowPanel(bool playAnimation = true)
        {
            base.ShowPanel(playAnimation);
            _view.SetTutorialToggle(_playerOptionsData.ShowTutorial);
            _view.StartMolesAnimationLoop();
        }
        
        protected override void OnViewLeft()
        {
            _view.Reset();
        }
    }
}
