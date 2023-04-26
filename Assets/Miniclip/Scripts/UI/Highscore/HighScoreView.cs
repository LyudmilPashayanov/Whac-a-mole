using System;
using System.Collections.Generic;
using Miniclip.Pooler;
using UnityEngine;
using UnityEngine.UI;

namespace Miniclip.UI.HighScore
{
    public class HighScoreView : MonoBehaviour
    {
        [SerializeField] private PoolController _pool;
        [SerializeField] private HighScoreField highScoreFieldPrefab;
        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private Button _playAgainButton;

        public void Subscribe(Action mainMenuClicked, Action playAgainClicked)
        {
            _mainMenuButton.onClick.AddListener(()=>
            {
                mainMenuClicked?.Invoke();
            });
            _playAgainButton.onClick.AddListener(()=>
            {
                playAgainClicked?.Invoke();
            });
            
        }
        
        public void UpdateScrollView(List<IPoolData> attempts)
        {
            _pool.UpdatePooler(attempts,true, highScoreFieldPrefab.RectTransform);
        }
    }
}
