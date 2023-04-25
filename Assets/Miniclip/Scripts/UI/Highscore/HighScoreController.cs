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
        }

        private void UpdateBoard(List<AttemptData> attemptData)
        {
            _view.UpdateScrollView(attemptData.ToList<IPoolData>());
        }

        public override void ShowPanel(bool playAnimation = true)
        {
            base.ShowPanel(playAnimation);
        }

        protected override void OnViewLeft()
        {
            throw new System.NotImplementedException();
        }
    }
}
