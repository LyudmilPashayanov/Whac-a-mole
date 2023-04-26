using System;
using System.Collections.Generic;
using System.Linq;
using Miniclip.Entities;
using Miniclip.Pooler;
using UnityEngine;

namespace Miniclip.UI.HighScore
{
    /// <summary>
    /// This class is responsible for the High Score Panel in Whac-A-Mole.
    /// All the logic for the High scores is handled here.
    /// </summary>
    public class HighScoreController : UIPanel
    {
        #region Variables

        [SerializeField] public HighScoreView _view;
        
        private PlayerData _playerData;
        
        private bool _showingLocally = false;
        
        private Action<Action<WorldsData>> _worldsDataRequest;

        #endregion

        #region Functionality

        
      public void Init(PlayerData playerData, Action<Action<WorldsData>> worldsDataRequest)
        {
            _playerData = playerData;
            _worldsDataRequest = worldsDataRequest;
            _view.Subscribe(GoToMainMenu, PlayAgain, ShowLocalHighScore, ShowWorldsHighScore);
        }
        
        /// <summary>
        /// Called initially when you navigate to the view.
        /// </summary>
        public void SetupBoard()
        {
            _view.EnableLoadingScreen(false);
            if (_showingLocally==false)
            {
                _showingLocally = true;
                _view.LocalButtonClicked();
                _view.SetLocalInfo();
                _view.SetupScrollView(ConvertAttemptDataToUIData(_playerData.PlayerAttempts));
            }
            else
            {
                _view.LocalButtonClicked();
                _view.SetLocalInfo();
                UpdateBoard(_playerData.PlayerAttempts,true);
            }
        }
        
        /// <summary>
        /// Updates the board with the given data.
        /// </summary>
        /// <param name="attemptData">The data you want to update the board with</param>
        /// <param name="force">If true: deletes all entries in the board and re-instantiates them. If false: Just re-arranges the items</param>
        private void UpdateBoard(List<AttemptData> attemptData, bool force)
        {
            _view.UpdateScrollView(ConvertAttemptDataToUIData(attemptData),force);
        }

        /// <summary>
        /// Converts the actual data class to a UI data class, which can be used in the score board.
        /// </summary>
        /// <param name="attemptData">The data you want to convert.</param>
        /// <returns></returns>
        private List<PoolData> ConvertAttemptDataToUIData(List<AttemptData> attemptData)
        {
            AttemptData currentAttempt = attemptData.Last();
            List<AttemptData> shallowSortedData = attemptData.GetRange(0, attemptData.Count);
            shallowSortedData.Sort( (a,b) => b.Score.CompareTo(a.Score));
            List<AttemptDataUI> uiDataList = new List<AttemptDataUI>();
            for (int i = 0; i < shallowSortedData.Count; i++)
            {
                uiDataList.Add(new AttemptDataUI(shallowSortedData[i].Score,shallowSortedData[i].Name,i+1, currentAttempt == shallowSortedData[i]));
            }

            return uiDataList.ToList<PoolData>();
        }

        private void ShowLocalHighScore()
        {
            if (_showingLocally == false)
            {
                _showingLocally = true;
                _view.LocalButtonClicked();
                _view.SetLocalInfo();
                UpdateBoard(_playerData.PlayerAttempts,true);
            }
        }

        private void ShowWorldsHighScore()
        {
            if (_showingLocally)
            {
                _showingLocally = false;
                _view.WorldsButtonClicked();
                _view.EnableLoadingScreen(true);
                _view.SetWorldsInfo();
                _worldsDataRequest?.Invoke(OnWorldsDataRetrieved);
            }
        }
        
        private void PlayAgain()
        {
            Owner.SwitchPanel(Panel.Gameplay);
        }
        
        private void GoToMainMenu()
        {
            Owner.SwitchPanel(Panel.MainMenu);
        }

        #endregion

        #region EventHandlers

        /// <summary>
        /// Called when the global leaderboards are retrieved from PlayFab.
        /// Then it shows the data retrieved in the high score panel.
        /// </summary>
        /// <param name="data"></param>
        private void OnWorldsDataRetrieved(WorldsData data)
        {
            List<AttemptData> shallowCopy = data.worldWideAttempts.GetRange(0, data.worldWideAttempts.Count);
            AttemptData currentAttempt = _playerData.PlayerAttempts.Last();

            if (_playerData.IsAttemptRecord(currentAttempt))
            {
                // Your current attempt will appear in the top 10
                AttemptData recordData = new AttemptData();
                for (int i = 0; i < data.worldWideAttempts.Count; i++)
                {
                    if (data.worldWideAttempts[i].Name == currentAttempt.Name &&
                        data.worldWideAttempts[i].Score == currentAttempt.Score)
                    {
                        recordData = data.worldWideAttempts[i];
                        break;
                    }
                }

                data.worldWideAttempts.Remove(recordData);
                data.worldWideAttempts.Add(recordData);
                UpdateBoard(shallowCopy, true);
                _view.EnableLoadingScreen(false);
            }
            else
            {
                // Your current attempt will appear at the end.
                shallowCopy.Add(currentAttempt);
                UpdateBoard(shallowCopy, true);
                _view.EnableLoadingScreen(false);
            }
        }
        
        #endregion

        #region EventHandlers

        protected override void OnViewLeft()
        {
            ShowLocalHighScore();
        }

        #endregion
      
    }
}
