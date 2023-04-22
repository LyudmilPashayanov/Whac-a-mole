using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Miniclip.UI.Tutorial
{
    public class TutorialView : MonoBehaviour
    {
        [SerializeField] private RectTransform _normalMoleSprite;
        [SerializeField] private RectTransform _fortifiedMoleSprite;
        [SerializeField] private RectTransform _bombMoleSprite;
        [SerializeField] private Toggle _tutorialToggle;
        [SerializeField] private Button _startButton;
        
        private Sequence _molesLoop;
        
        public void Subscribe(Action StartGameplay)
        {
            _startButton.onClick.AddListener(() => StartGameplay?.Invoke());
        }
        
        public void StartMolesAnimationLoop()
        {
            _molesLoop = DOTween.Sequence();
            _molesLoop.Append(_normalMoleSprite.DOScale(1.2f, 1f).SetLoops(4,LoopType.Yoyo));
            _molesLoop.Append(_fortifiedMoleSprite.DOScale(1.2f, 1f).SetLoops(4,LoopType.Yoyo));
            _molesLoop.Append(_bombMoleSprite.DOScale(1.2f, 1f).SetLoops(4,LoopType.Yoyo));
            _molesLoop.SetLoops(-1);
        }

        public void StopMolesAnimationLoop()
        {
            _molesLoop.Kill();
        }

        public bool GetTutorialAgainCheck()
        {
            return !_tutorialToggle.isOn;
        }
    }
}
