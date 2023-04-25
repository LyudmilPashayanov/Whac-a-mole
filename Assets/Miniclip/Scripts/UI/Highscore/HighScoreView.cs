using System.Collections.Generic;
using Miniclip.Pooler;
using UnityEngine;

namespace Miniclip.UI.HighScore
{
    public class HighScoreView : MonoBehaviour
    {
        [SerializeField] private PoolController _pool;
        [SerializeField] private HighScoreField highScoreFieldPrefab;

        public void UpdateScrollView(List<IPoolData> attempts)
        {
            _pool.Setup(attempts, highScoreFieldPrefab.RectTransform);
        }
    }
}
