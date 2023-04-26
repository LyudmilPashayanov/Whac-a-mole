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
        [SerializeField] private HighScoreField _highScoreFieldPrefab;
        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private Button _playAgainButton;
        [SerializeField] private Button _getLocalDataButton;
        [SerializeField] private Button _getWorldsDataButton;
        private readonly Color _clickedColor = Color.gray;
        private readonly Color _normalColor = Color.white;
        
        public void Subscribe(Action mainMenuClicked, Action playAgainClicked, Action localDataButtonClicked, Action worldsDataButtonClicked)
        {
            _mainMenuButton.onClick.AddListener(()=>
            {
                mainMenuClicked?.Invoke();
            });
            _playAgainButton.onClick.AddListener(()=>
            {
                playAgainClicked?.Invoke();
            });
            
            _getLocalDataButton.onClick.AddListener(()=>
            {
                localDataButtonClicked?.Invoke();
            });
            
            _getWorldsDataButton.onClick.AddListener(()=>
            {
                worldsDataButtonClicked?.Invoke();
            });
        }
        
        public void UpdateScrollView(List<IPoolData> attempts, bool forceUpdate)
        {
            _pool.UpdatePooler(attempts,forceUpdate, _highScoreFieldPrefab.RectTransform);
        } 
        
        public void SetupScrollView(List<IPoolData> attempts)
        {
            _pool.Setup(attempts, _highScoreFieldPrefab.RectTransform);
        }

        public void LocalButtonClicked()
        {
            _getLocalDataButton.image.color = _clickedColor;
            _getWorldsDataButton.image.color = _normalColor;
        }
        
        public void WorldsButtonClicked()
        {
            _getWorldsDataButton.image.color = _clickedColor;
            _getLocalDataButton.image.color = _normalColor;

        }
    }
}
