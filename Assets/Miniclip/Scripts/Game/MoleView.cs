using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Miniclip.Game
{
    public class MoleView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image _moleImage;
        [SerializeField] private RectTransform _bomb;
        [SerializeField] private RectTransform _helmet;

        private float _showingDuration = 0.7f;
        private Sequence _moleAnimation;
        public event Action OnMoleClicked;
        public event Action OnMoleHidden;

        private void Start()
        {
            _moleAnimation = DOTween.Sequence();
        }

        public void EnableBomb(bool enable)
        {
            _bomb.gameObject.SetActive(enable);
        }
    
        public void EnableHelmet(bool enable)
        {
            _helmet.gameObject.SetActive(enable);
        }
    
        public void SetSprite(Sprite sprite)
        {
            _moleImage.sprite = sprite;
        }
        
        public void StartExplosion(Action moleDie)
        {
            // show animation for explosion
            
            moleDie?.Invoke();
        }

        public void ShowMole(float hideAfterTime)
        {
            _moleAnimation.Kill();
            _moleAnimation = DOTween.Sequence();
            _moleAnimation.Append(transform.DOScale(1.5f, _showingDuration));
            _moleAnimation.Insert(0, transform.DOLocalMove(new Vector2(0, 150), _showingDuration));
            _moleAnimation.AppendInterval(hideAfterTime).OnComplete(() =>
            {
                HideMole();
            });
        }
        
        public void HideMole()
        {
            _moleAnimation.Kill();
            _moleAnimation = DOTween.Sequence();
            _moleAnimation.Append(transform.DOScale(0, _showingDuration));
            _moleAnimation.Insert(0, transform.DOLocalMove(Vector2.zero, _showingDuration));
            _moleAnimation.OnComplete(() =>
            {
                OnMoleHidden?.Invoke();
            });
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (OnMoleClicked != null)
            {
                OnMoleClicked();
            }
        }
    }
}
