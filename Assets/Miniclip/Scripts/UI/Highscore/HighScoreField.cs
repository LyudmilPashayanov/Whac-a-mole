using Miniclip.Pooler;
using TMPro;
using UnityEngine;

namespace Miniclip.UI.HighScore
{
    public class HighScoreField : MonoBehaviour, IPoolFields
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _score;
        [SerializeField] public RectTransform RectTransform;

        public void UpdateField(IPoolData highScoreField)
        {
            AttemptData data = (AttemptData)highScoreField; 
            _name.text = data.name;
            _score.text = data.score.ToString();
        }
    }
}
