using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Miniclip.Pooler
{
    /// <summary>
    /// Marks a Scroll View as one to use the UI pooling technique, in order to save performance.
    /// </summary>
    public class PoolController : MonoBehaviour, IBeginDragHandler
    {
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private RectTransform _viewPort;
        [SerializeField] private RectTransform _dragDetection;
        [SerializeField] private RectTransform _content;
        
        private float _itemHeight;
        [FormerlySerializedAs("BufferSize")]
        [Tooltip("How much extra items to put in the _content, so that fast scrolling doesn't show emptiness")]
        [SerializeField] private int _bufferSize;
        private List<IPoolData> _pool;
        private int _poolHead;
        private int _poolTail;
        private float _dragDetectionAnchorPreviousY = 0;

        /// <summary>
        /// Calculates how many items from the "_pool" list can be visible in the _content section. 
        /// </summary>
        int TargetVisibleItemCount { get { return Mathf.Max(Mathf.CeilToInt(_viewPort.rect.height / _itemHeight), 0); } }
        int TopItemOutOfView { get { return Mathf.CeilToInt(_content.anchoredPosition.y / _itemHeight); } }

        /// <summary>
        /// Optimized way to update the fields in the scroll view.  
        /// </summary>
        /// <param name="list">The list with which the scroll view will be updated.</param>
        /// <param name="forceUpdate">Pass "true" if you want to forcefully update the scroll view, but heavy on performance.</param>
        /// <param name="prefab">The prefab which will be used to update the list.</param>
        public void UpdatePooler(List<IPoolData> list,bool forceUpdate, RectTransform prefab = null)
        {
            if (forceUpdate || list.Count < TargetVisibleItemCount + _bufferSize)
            {
                _scrollRect.onValueChanged.RemoveAllListeners();
                foreach (Transform child in _content)
                {
                    Destroy(child.gameObject);
                }
                Setup(list,prefab); 
                _dragDetection.anchoredPosition = Vector2.zero;
                _content.DOAnchorPos(Vector2.zero, 0.1f);
                return;
            }
            _scrollRect.StopMovement();
            _pool = list;
            // Moves the scroll content to the top.
            _poolTail = 0;
            _poolHead = 0;
            _dragDetection.sizeDelta = new Vector2(_dragDetection.sizeDelta.x, _pool.Count * _itemHeight);
            _dragDetection.anchoredPosition = Vector2.zero;
            foreach (Transform child in _content)
            {
                child.GetComponent<IPoolFields>().UpdateField(_pool[_poolTail]);
                _poolTail++;
            }
            _content.DOAnchorPos(Vector2.zero,0.1f);
        }
        
        /// <summary>
        /// Creates a scroll view with content from the passed "list" parameter. The content is presented in an optimized way, via object pooling technique.
        /// </summary>
        /// <param name="list">The list with which the scroll view will be filled</param>
        /// <param name="prefab">A prefab with "IPoolField" attached to it, which will be shown in the scroll view.</param>
        public void Setup(List<IPoolData> list, RectTransform prefab)
        {
            _poolHead = 0;
            _poolTail = 0;
            _pool = list;
            _itemHeight = prefab.rect.height;
            _scrollRect.onValueChanged.AddListener(OnDragDetectionPositionChange);
            _dragDetection.sizeDelta = new Vector2(_dragDetection.sizeDelta.x, _pool.Count * _itemHeight);
            for (int i = 0; i < TargetVisibleItemCount+_bufferSize; i++)
            {
                if (_pool.Count - 1 < i) break;
                RectTransform item = Instantiate(prefab, _content, true);
                item.transform.localScale = Vector3.one;
                item.GetComponent<IPoolFields>().UpdateField(_pool[_poolTail]);
                _poolTail++;
            }
        }

        /// <summary>
        /// Constantly checks when being dragged to see if needed to update the _content.
        /// </summary>
        /// <param name="dragNormalizePos"></param>
        private void OnDragDetectionPositionChange(Vector2 dragNormalizePos)
        {
            float dragDelta = _dragDetection.anchoredPosition.y - _dragDetectionAnchorPreviousY;
            Vector2 anchoredPosition = _content.anchoredPosition;
            anchoredPosition = new Vector2(anchoredPosition.x, anchoredPosition.y + dragDelta);
            _content.anchoredPosition = anchoredPosition;
            UpdateContentBuffer();
            _dragDetectionAnchorPreviousY = _dragDetection.anchoredPosition.y;
        }

        /// <summary>
        /// Checks what is in the view and updates to show relevant data.
        /// </summary>
        private void UpdateContentBuffer()
        {
            if (TopItemOutOfView > _bufferSize)
            {
                if (_poolTail >= _pool.Count)
                {
                    return;
                }

                Transform firstChild = _content.GetChild(0);
                firstChild.SetSiblingIndex(_content.childCount - 1);
                firstChild.gameObject.GetComponent<IPoolFields>().UpdateField(_pool[_poolTail]);
                Vector2 anchoredPosition = _content.anchoredPosition;
                _content.anchoredPosition = new Vector2(anchoredPosition.x, anchoredPosition.y - firstChild.GetComponent<RectTransform>().rect.height);
                _poolHead++;
                _poolTail++;
            }
            else if (TopItemOutOfView < _bufferSize)
            {
                if (_poolHead <= 0)
                {
                    return;
                }

                Transform lastChild = _content.GetChild(_content.childCount - 1);
                lastChild.SetSiblingIndex(0);
                _poolHead--;
                _poolTail--;
                lastChild.gameObject.GetComponent<IPoolFields>().UpdateField(_pool[_poolHead]);
                Vector2 anchoredPosition = _content.anchoredPosition;
                _content.anchoredPosition = new Vector2(anchoredPosition.x, anchoredPosition.y + lastChild.GetComponent<RectTransform>().rect.height);

            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _dragDetectionAnchorPreviousY = _dragDetection.anchoredPosition.y;
        }
    }
}
