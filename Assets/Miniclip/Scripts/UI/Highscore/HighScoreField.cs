using Miniclip.Entities;
using Miniclip.Pooler;
using TMPro;
using UnityEngine;

namespace Miniclip.UI.HighScore
{
    public class HighScoreField : MonoBehaviour, IPoolFields
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _score;
        [SerializeField] private TMP_Text _placement;
        [SerializeField] public RectTransform RectTransform;

        public void UpdateField(IPoolData highScoreField)
        {
            AttemptDataUI data = (AttemptDataUI)highScoreField; 
            _name.text = data.Name;
            _score.text = data.Score.ToString();
            _placement.text = data.Placement.ToString();
        }
    }
}
