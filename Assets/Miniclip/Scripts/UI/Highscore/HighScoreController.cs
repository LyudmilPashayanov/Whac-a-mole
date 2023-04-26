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

        public void Init(PlayerData playerData)
        {
            _playerData = playerData;
            _view.Subscribe(GoToMainMenu, PlayAgain);
        }

        public void ShowWorldsHighScore()
        {
            // access playfab to get it maybe?
        }
        
        public void ShowLocalHighScore()
        {
            UpdateBoard(_playerData.PlayerAttempts);
        }
        
        private void UpdateBoard(List<AttemptData> attemptData)
        {
            _view.UpdateScrollView(ConvertAttemptDataToUIData(attemptData));
        }

        private List<IPoolData> ConvertAttemptDataToUIData(List<AttemptData> attemptData)
        {
            attemptData.Sort( (a,b) => b.Score.CompareTo(a.Score));
            List<AttemptDataUI> uiDataList = new List<AttemptDataUI>();
            for (int i = 0; i < attemptData.Count; i++)
            {
                uiDataList.Add(new AttemptDataUI(attemptData[i].Score,attemptData[i].Name,i+1));
            }

            return uiDataList.ToList<IPoolData>();
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
            // Reset the stuff in the view if needed
        }
    }
}
