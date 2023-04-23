using Miniclip.Audio;
using UnityEngine;

namespace Miniclip.UI.Tutorial
{
    public class TutorialController : UIPanel
    {
        [SerializeField] private TutorialView _view;

        private void Start()
        {
            _view.Subscribe(StartGameplay);
        }

        private void StartGameplay()
        {
            AudioManager.Instance.PlayButtonClickSound();
            Owner.SwitchPanel(Panel.Gameplay);
        }
        
        public bool GetTutorialAgain()
        {
            return _view.GetTutorialAgainCheck();
        }

        public override void ShowPanel(bool playAnimation = true)
        {
            base.ShowPanel(playAnimation);
            _view.StartMolesAnimationLoop();
        }
        
        protected override void OnViewLeft()
        {
            _view.Reset();
        }
    }
}
