using System.Collections.Generic;
using Miniclip.Entities;
using Miniclip.Pooler;
using Unity.VisualScripting;
using UnityEngine;

namespace Miniclip.UI.HighScore
{
    public class HighScoreController : UIPanel
    {
        [SerializeField] public HighScoreView _view;

        private void Start()
        {
            //_view.UpdateScrollView(asds);
        }

        private void UpdateBoard(List<AttemptData> attemptData)
        {
            AttemptData asd = new AttemptData();
            asd.Name = "asd";
            asd.Score = 100;
            
            AttemptData asd2 = new AttemptData();
            asd2.Name = "asd";
            asd2.Score = 100;
            
            AttemptData asd3 =new AttemptData();
            asd3.Name = "asd";
            asd3.Score = 100;
            
            AttemptData asd4 = new AttemptData();
            asd4.Name = "asd";
            asd4.Score = 100;
            
            List<IPoolData> asds = new List<IPoolData>();
            asds.Add(asd);
            asds.Add(asd2);
            asds.Add(asd3);
            asds.Add(asd4);
            
            _view.UpdateScrollView(asds);        
            
            
        }
        
        protected override void OnViewLeft()
        {
            throw new System.NotImplementedException();
        }
    }
}
