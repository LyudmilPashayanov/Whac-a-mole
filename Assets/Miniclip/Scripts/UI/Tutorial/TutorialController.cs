using System;
using UnityEngine;

namespace Miniclip.UI.Tutorial
{
    public class TutorialController : UIPanel
    {
        [SerializeField] private TutorialView _view;

        private void Start()
        {
            _view.StartMolesAnimationLoop();
        }

        public bool GetTutorialAgain()
        {
            return _view.GetTutorialAgainCheck();
        }
    }
}
