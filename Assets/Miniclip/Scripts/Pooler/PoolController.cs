using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Miniclip.Pooler
{
    /// <summary>
    /// Marks a Scroll View as one to use the UI pooling technique, in order to save performance.
    /// </summary>
    public class PoolController : MonoBehaviour, IBeginDragHandler
    {
        [SerializeField] private ScrollRect ScrollRect;
        [SerializeField] private RectTransform ViewPort;
        [SerializeField] private RectTransform DragDetection;
        [SerializeField] private RectTransform Content;
        private float ItemHeight;
        [Tooltip("How much extra items to put in the Content, so that fast scrolling doesn't show emptiness")]
        [SerializeField] private int BufferSize;
        private List<IPoolData> Pool;
        private int PoolHead;
        private int PoolTail;
        float DragDetectionAnchorPreviousY = 0;

        /// <summary>
        /// Calculates how many items from the "Pool" list can be visible in the Content section. 
        /// </summary>
        int TargetVisibleItemCount { get { return Mathf.Max(Mathf.CeilToInt(ViewPort.rect.height / ItemHeight), 0); } }
        int TopItemOutOfView { get { return Mathf.CeilToInt(Content.anchoredPosition.y / ItemHeight); } }

        /// <summary>
        /// Creates a scroll view with content from the passed "list" parameter. The content is presented in an optimized way, via object pooling technique.
        /// </summary>
        /// <param name="list">The list with which the scroll view will be filled</param>
        /// <param name="prefab">A prefab with "IPoolField" attached to it, which will be shown in the scroll view.</param>
        public void Setup(List<IPoolData> list, RectTransform prefab)
        {
            PoolHead = 0;
            PoolTail = 0;
            Pool = list;
            ItemHeight = prefab.rect.height;
            ScrollRect.onValueChanged.AddListener(OnDragDetectionPositionChange);
            DragDetection.sizeDelta = new Vector2(DragDetection.sizeDelta.x, Pool.Count * ItemHeight);
            for (int i = 0; i < TargetVisibleItemCount+BufferSize; i++)
            {
                if (Pool.Count - 1 < i) break;
                RectTransform itemGO = Instantiate(prefab);
                itemGO.transform.SetParent(Content);
                itemGO.transform.localScale = Vector3.one;
                itemGO.GetComponent<IPoolFields>().UpdateField(Pool[PoolTail]);
                PoolTail++;
            }
        }

        /// <summary>
        /// Optimized way to update the fields in the scroll view.  
        /// </summary>
        /// <param name="list">The list with which the scroll view will be updated.</param>
        /// <param name="forceUpdate">Pass "true" if you want to forcefully update the scroll view, but heavy on performance.</param>
        /// <param name="prefab">The prefab which will be used to update the list.</param>
        public void UpdatePooler(List<IPoolData> list,bool forceUpdate, RectTransform prefab = null)
        {
            if (forceUpdate || list.Count < TargetVisibleItemCount + BufferSize)
            {
                ScrollRect.onValueChanged.RemoveAllListeners();
                foreach (Transform child in Content)
                {
                    Destroy(child.gameObject);
                }
                Setup(list,prefab); 
                DragDetection.anchoredPosition = Vector2.zero;
                Content.DOAnchorPos(Vector2.zero, 0.1f);
                return;
            }
       
            Pool = list;
            // Moves the scroll content to the top.
            PoolTail = 0;
            PoolHead = 0;
            DragDetection.sizeDelta = new Vector2(DragDetection.sizeDelta.x, Pool.Count * ItemHeight);
            DragDetection.anchoredPosition = Vector2.zero;
            foreach (Transform child in Content)
            {
                child.GetComponent<IPoolFields>().UpdateField(Pool[PoolTail]);
                PoolTail++;
            }
            Content.DOAnchorPos(Vector2.zero,0.1f);
        }

        /// <summary>
        /// Constantly checks when being dragged to see if needed to update the Content.
        /// </summary>
        /// <param name="dragNormalizePos"></param>
        public void OnDragDetectionPositionChange(Vector2 dragNormalizePos)
        {
            float dragDelta = DragDetection.anchoredPosition.y - DragDetectionAnchorPreviousY;
            Content.anchoredPosition = new Vector2(Content.anchoredPosition.x, Content.anchoredPosition.y + dragDelta);
            UpdateContentBuffer();
            DragDetectionAnchorPreviousY = DragDetection.anchoredPosition.y;
        }

        /// <summary>
        /// Checks what is in the view and updates to show relevant data.
        /// </summary>
        private void UpdateContentBuffer()
        {
            if (TopItemOutOfView > BufferSize)
            {
                if (PoolTail >= Pool.Count)
                {
                    return;
                }

                Transform firstChild = Content.GetChild(0);
                firstChild.SetSiblingIndex(Content.childCount - 1);
                firstChild.gameObject.GetComponent<IPoolFields>().UpdateField(Pool[PoolTail]);
                Content.anchoredPosition = new Vector2(Content.anchoredPosition.x, Content.anchoredPosition.y - firstChild.GetComponent<RectTransform>().rect.height);
                PoolHead++;
                PoolTail++;
            }
            else if (TopItemOutOfView < BufferSize)
            {
                if (PoolHead <= 0)
                {
                    return;
                }

                Transform lastChild = Content.GetChild(Content.childCount - 1);
                lastChild.SetSiblingIndex(0);
                PoolHead--;
                PoolTail--;
                lastChild.gameObject.GetComponent<IPoolFields>().UpdateField(Pool[PoolHead]);
                Content.anchoredPosition = new Vector2(Content.anchoredPosition.x, Content.anchoredPosition.y + lastChild.GetComponent<RectTransform>().rect.height);

            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            DragDetectionAnchorPreviousY = DragDetection.anchoredPosition.y;
        }
    }
}
