using UnityEngine;

namespace Miniclip.UI
{
    public abstract class UIPanel : MonoBehaviour
    {
        public UIManager owner;
        public virtual void Init(UIManager owner)
        {
            this.owner = owner;
        }
    }
}