using TMPro;
using UnityEngine;

namespace Miniclip.UI.Highscore
{
    public class HighscoreField : MonoBehaviour
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _score;
        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = transform.GetComponent<RectTransform>();
        }

        public void Init(RectTransform parent, string name, string score)
        {
            _rectTransform.SetParent(parent,false);
            
        }
    }
}
