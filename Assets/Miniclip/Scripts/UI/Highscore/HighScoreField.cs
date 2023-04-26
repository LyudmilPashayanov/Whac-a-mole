using Miniclip.Entities;
using Miniclip.Pooler;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Miniclip.UI.HighScore
{
    public class HighScoreField : MonoBehaviour, IPoolFields
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _score;
        [SerializeField] private TMP_Text _placement;
        [SerializeField] private Image _image;
        [SerializeField] public RectTransform RectTransform;
        
        private Color _highlightedColor = Color.green;
        private Color _normalColor = Color.yellow;

        public void UpdateField(IPoolData highScoreField)
        {
            AttemptDataUI data = (AttemptDataUI)highScoreField; 
            _name.text = data.Name;
            _score.text = data.Score.ToString();
            _placement.text = data.Placement.ToString();
            if (data.Highlighted)
                _image.color = _highlightedColor;
            else
                _image.color = _normalColor;
        }
    }
}
