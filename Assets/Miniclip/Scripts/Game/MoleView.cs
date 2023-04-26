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
        private Sequence _moleShowingSequence;
        private Sequence _moleHidingSequence;
        public event Action OnMoleClicked;
        public event Action OnMoleHidden;

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
            _moleShowingSequence = DOTween.Sequence();
            _moleShowingSequence.Append(transform.DOScale(1.5f, _showingDuration));
            _moleShowingSequence.Insert(0, transform.DOLocalMove(new Vector2(0, 150), _showingDuration));
            _moleShowingSequence.AppendInterval(hideAfterTime);
            _moleShowingSequence.OnComplete(() =>
            {
                HideMole();
            });
        }
        
        public void HideMole()
        {
            if (_moleShowingSequence.IsPlaying())
            {
                _moleShowingSequence.Kill(false);
            }
            _moleHidingSequence = DOTween.Sequence();
            _moleHidingSequence.Append(transform.DOScale(0, _showingDuration));
            _moleHidingSequence.Insert(0, transform.DOLocalMove(Vector2.zero, _showingDuration));
            _moleHidingSequence.OnComplete(() =>
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

        public void PauseMole()
        {
            _moleHidingSequence.Pause();
            _moleShowingSequence.Pause();
        }
        
        public void UnpauseMole()
        {
            _moleHidingSequence.PlayForward();
            _moleShowingSequence.PlayForward();
        }

        public void EnableInteractable(bool enable)
        {
            _moleImage.raycastTarget = enable;
        }
    }
}
