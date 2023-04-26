using System;
using System.Collections.Generic;
using System.Linq;
using Miniclip.Entities;
using Miniclip.Pooler;
using UnityEngine;

namespace Miniclip.UI.HighScore
{
    public class HighScoreController : UIPanel
    {
        [SerializeField] public HighScoreView _view;
        private PlayerData _playerData;
        private bool _showingLocally = false;
        private Action<Action<WorldsData>> _worldsDataRequest; 

        public void Init(PlayerData playerData, Action<Action<WorldsData>> worldsDataRequest)
        {
            _playerData = playerData;
            _worldsDataRequest = worldsDataRequest;
            _view.Subscribe(GoToMainMenu, PlayAgain, ShowLocalHighScore, ShowWorldsHighScore);
        }
        
        public void SetupBoard()
        {
            _view.EnableLoadingScreen(false);
            if (_showingLocally==false)
            {
                _showingLocally = true;
                _view.LocalButtonClicked();
                _view.SetupScrollView(ConvertAttemptDataToUIData(_playerData.PlayerAttempts));
            }
            else
            {
                _view.LocalButtonClicked();
                UpdateBoard(_playerData.PlayerAttempts,true);
            }
        }
        
        private void UpdateBoard(List<AttemptData> attemptData, bool force)
        {
            _view.UpdateScrollView(ConvertAttemptDataToUIData(attemptData),force);
        }

        private List<IPoolData> ConvertAttemptDataToUIData(List<AttemptData> attemptData)
        {
            AttemptData currentAttempt = attemptData.Last();
            List<AttemptData> shallowSortedData = attemptData.GetRange(0, attemptData.Count);
            shallowSortedData.Sort( (a,b) => b.Score.CompareTo(a.Score));
            List<AttemptDataUI> uiDataList = new List<AttemptDataUI>();
            for (int i = 0; i < shallowSortedData.Count; i++)
            {
                uiDataList.Add(new AttemptDataUI(shallowSortedData[i].Score,shallowSortedData[i].Name,i+1, currentAttempt == shallowSortedData[i]));
            }

            return uiDataList.ToList<IPoolData>();
        }

        private void ShowLocalHighScore()
        {
            if (_showingLocally == false)
            {
                _showingLocally = true;
                _view.LocalButtonClicked();
                UpdateBoard(_playerData.PlayerAttempts,false);
            }
        }

        private void ShowWorldsHighScore()
        {
            if (_showingLocally)
            {
                _showingLocally = false;
                _view.WorldsButtonClicked();
                _view.EnableLoadingScreen(true);
                _worldsDataRequest?.Invoke(OnWorldsDataRetrieved);
            }
        }
        
        private void OnWorldsDataRetrieved(WorldsData data)
        {
            AttemptData currentAttempt = _playerData.PlayerAttempts.Last();
            List<AttemptData> shallowCopy = data.worldWideAttempts.GetRange(0, data.worldWideAttempts.Count);
            shallowCopy.Add(currentAttempt);
            
            UpdateBoard(shallowCopy,true);
            _view.EnableLoadingScreen(false);
        }
        
        private void PlayAgain()
        {
            Owner.SwitchPanel(Panel.Gameplay);
        }
        
        private void GoToMainMenu()
        {
            Owner.SwitchPanel(Panel.MainMenu);
        }

        protected override void OnViewLeft()
        {
            ShowLocalHighScore();
        }
    }
}
