using System.Collections.Generic;
using System.Linq;
using Miniclip.Entities;
using Miniclip.Pooler;
using Unity.VisualScripting;
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
            _view.UpdateScrollView(attemptData.ToList<IPoolData>());
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
