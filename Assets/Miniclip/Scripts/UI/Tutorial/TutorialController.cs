using Miniclip.Audio;
using UnityEngine;

namespace Miniclip.UI.Tutorial
{
    public class TutorialController : UIPanel
    {
        [SerializeField] private TutorialView _view;

        private void Start()
        {
            _view.StartMolesAnimationLoop();
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
    }
}
