﻿using System;
using DG.Tweening;
using UnityEngine;

namespace Miniclip.UI
{
    /// <summary>
    /// Base class which marks that a controller is a UI Panel and that you can navigate to that panel.
    /// </summary>
    public abstract class UIPanel : MonoBehaviour
    {
        #region Variables

        protected UIManager Owner;
        
        [SerializeField] private CanvasGroup _canvasGroup;
        private Sequence _showAnimation;
        private Sequence _hideAnimation;
        public event Action OnHideComplete;
        public event Action OnShowComplete;

        #endregion

        #region Functionality

        
 protected void Awake()
        {
            _canvasGroup.alpha = 0f;
        }

        protected void OnDestroy()
        {
            _showAnimation.Kill();
            _hideAnimation.Kill();
        }
        
        public void Subscribe(UIManager owner)
        {
            Owner = owner;
        }

        public virtual void HidePanel(bool playAnimation = true)
        {
            _showAnimation?.Pause();
            if (playAnimation)
            {
                if (_hideAnimation == null)
                {
                    _canvasGroup.alpha = 1f;
                    _hideAnimation = DOTween.Sequence();
                    _hideAnimation.SetAutoKill(false);
                    _hideAnimation.Append(
                        _canvasGroup.DOFade(0f, 1)
                            .SetEase(Ease.InQuint)
                    );
                    _hideAnimation.OnComplete(HideCompleted);
                }
                else
                {
                    _hideAnimation.Restart();
                }
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
        
        public virtual void ShowPanel(bool playAnimation = true)
        {
            gameObject.SetActive(true);

            _showAnimation?.Pause();

            if (playAnimation)
            {
                if (_showAnimation == null)
                {
                    _showAnimation = DOTween.Sequence();
                    _showAnimation.SetAutoKill(false);
                    _showAnimation.Append(
                        _canvasGroup.DOFade(1f, 1)
                            .SetEase(Ease.InQuint)
                    );
                    _showAnimation.OnComplete(ShowCompleted);
                }
                else
                {
                    _showAnimation.Restart();
                }
            }
        }

        private void ShowCompleted()
        {
            OnShowComplete?.Invoke();
        }
        
        private void HideCompleted()
        {
            gameObject.SetActive(false);
            OnViewLeft();
            OnHideComplete?.Invoke();
        }

        #endregion

        #region Event Handlers

        protected abstract void OnViewLeft();

        #endregion
    }
}