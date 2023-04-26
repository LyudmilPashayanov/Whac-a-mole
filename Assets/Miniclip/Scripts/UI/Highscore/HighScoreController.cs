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
        
        public void Init(PlayerData playerData)
        {
            _playerData = playerData;
            _view.Subscribe(GoToMainMenu, PlayAgain, ShowLocalHighScore, ShowWorldsHighScore);
        }
        
        public void SetupBoard()
        {
            _showingLocally = true;
            _view.LocalButtonClicked();
            _view.SetupScrollView(ConvertAttemptDataToUIData(_playerData.PlayerAttempts));
        }
        
        private void UpdateBoard(List<AttemptData> attemptData)
        {
            _view.UpdateScrollView(ConvertAttemptDataToUIData(attemptData),false);
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
                UpdateBoard(_playerData.PlayerAttempts);
            }
        }

        private void ShowWorldsHighScore()
        {
            if (_showingLocally)
            {
                _showingLocally = false;
                _view.WorldsButtonClicked();
                // UpdateBoard( <WORLDS DATA HERE> );
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
        
        protected override void OnViewLeft()
        {
            _showingLocally = false;
        }
    }
}
