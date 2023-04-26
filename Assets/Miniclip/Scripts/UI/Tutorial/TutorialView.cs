using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Miniclip.UI.Tutorial
{
    /// <summary>
    /// Handles all UI related logic in the Tutorial Panel. 
    /// </summary>
    public class TutorialView : MonoBehaviour
    {
        #region Variables

        [SerializeField] private RectTransform _normalMoleSprite;
        [SerializeField] private RectTransform _fortifiedMoleSprite;
        [SerializeField] private RectTransform _bombMoleSprite;
        [SerializeField] private Toggle _tutorialToggle;
        [SerializeField] private Button _startButton;
        
        private Sequence _molesLoop;

        #endregion

        #region Functionality

        private void Start()
        {
            _molesLoop = DOTween.Sequence();
            _molesLoop.Append(_normalMoleSprite.DOScale(1.2f, 1f).SetLoops(4,LoopType.Yoyo));
            _molesLoop.Append(_fortifiedMoleSprite.DOScale(1.2f, 1f).SetLoops(4,LoopType.Yoyo));
            _molesLoop.Append(_bombMoleSprite.DOScale(1.2f, 1f).SetLoops(4,LoopType.Yoyo));
            _molesLoop.SetLoops(-1);
        }

        public void Subscribe(Action StartGameplay)
        {
            _startButton.onClick.AddListener(() => StartGameplay?.Invoke());
        }
        
        public void StartMolesAnimationLoop()
        {
            _molesLoop.Play();
        }
        
        public void Reset()
        {
            StopMolesAnimationLoop();
        }

        public bool GetTutorialAgainCheck()
        {
            return !_tutorialToggle.isOn;
        }
        
        private void StopMolesAnimationLoop()
        {
            _molesLoop.Pause();
        }

        public void SetTutorialToggle(bool showTutorial)
        {
            _tutorialToggle.isOn = !showTutorial;
        }

        #endregion
    }
}
