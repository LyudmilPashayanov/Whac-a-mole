using System;
using System.Collections.Generic;
using Miniclip.Pooler;
using UnityEngine;

namespace Miniclip.UI.HighScore
{
    public class HighScoreView : MonoBehaviour
    {
        [SerializeField] private PoolController _pool;
        [SerializeField] private HighScoreField highScoreFieldPrefab;

        // private List<IPoolData> asd = new List<IPoolData>();
        public void UpdateScrollView(List<IPoolData> attempts)
        {
            _pool.Setup(attempts, highScoreFieldPrefab.RectTransform);
            //asd = attempts;
        }

        /*private void Update()
        {
            if(Input.GetKeyDown(KeyCode.A))
                _pool.Setup(asd, highScoreFieldPrefab.RectTransform);
        }*/
    }
}
