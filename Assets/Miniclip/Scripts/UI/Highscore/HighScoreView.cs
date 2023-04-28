using System;
using System.Collections.Generic;
using Miniclip.Pooler;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Miniclip.UI.HighScore
{
    /// <summary>
    /// Handles all UI related logic in the High Score Panel. 
    /// </summary>
    public class HighScoreView : MonoBehaviour
    {
        #region Variables

        private const string LOCAL_INFO = "In this mode you see only the attempts of \"Whac-a-mole\" played on this device. The one in green is your latest attempt.";
        private const string LOCAL_INFO_TITLE = "Device high score info:";
        private const string GLOBAL_INFO = "In this mode you see all the attempts of \"Whac-a-mole\" played worldwide. The one in green is your latest attempt.";
        private const string GLOBAL_INFO_TITLE = "World high score info:";
        
        [SerializeField] private PoolController _pool;
        [SerializeField] private HighScoreField _highScoreFieldPrefab;
        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private Button _playAgainButton;
        [SerializeField] private Button _getLocalDataButton;
        [SerializeField] private Button _getWorldsDataButton;
        [SerializeField] private GameObject _loadingScreen;
        [SerializeField] private TMP_Text _infoTitle;
        [SerializeField] private TMP_Text _infoText;
        private readonly Color _clickedColor = Color.gray;
        private readonly Color _normalColor = Color.white;

        #endregion

        #region Functionality

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
        
        public void UpdateScrollView(List<PoolData> attempts, bool forceUpdate)
        {
            _pool.UpdatePooler(attempts,forceUpdate, _highScoreFieldPrefab.RectTransform);
        } 
        
        public void SetupScrollView(List<PoolData> attempts)
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

        public void EnableLoadingScreen(bool enable)
        {
            _loadingScreen.gameObject.SetActive(enable);
        }

        public void SetLocalInfo()
        {
            _infoTitle.text = LOCAL_INFO_TITLE;
            _infoText.text = LOCAL_INFO;
        }
        
        public void SetWorldsInfo()
        {
            _infoTitle.text = GLOBAL_INFO_TITLE;
            _infoText.text = GLOBAL_INFO;
        }

        public void EnableButtons(bool enable)
        {
            _mainMenuButton.interactable = enable;
            _playAgainButton.interactable = enable;

        }
        
        #endregion
    }
}
