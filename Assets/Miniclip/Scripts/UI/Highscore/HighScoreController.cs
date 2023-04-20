using System.Collections.Generic;
using Miniclip.Pooler;
using UnityEngine;

namespace Miniclip.UI.HighScore
{
    public class HighScoreController : MonoBehaviour
    {
        [SerializeField] public HighScoreView _view;

        private void Start()
        {
            AttemptData asd = new AttemptData();
            asd.name = "asd";
            asd.score = 100;
            
            AttemptData asd2 = new AttemptData();
            asd2.name = "asd";
            asd2.score = 100;
            
            AttemptData asd3 = new AttemptData();
            asd3.name = "asd";
            asd3.score = 100;
            
            AttemptData asd4 = new AttemptData();
            asd4.name = "asd";
            asd4.score = 100;
            
            AttemptData asd5 = new AttemptData();
            asd5.name = "asd";
            asd5.score = 100;
            
            AttemptData asd6 = new AttemptData();
            asd6.name = "asd";
            asd6.score = 100;
            
            AttemptData asd7 = new AttemptData();
            asd7.name = "asd";
            asd7.score = 100;
            
            AttemptData asd8 = new AttemptData();
            asd8.name = "asd";
            asd8.score = 100;
            
            AttemptData asd9 = new AttemptData();
            asd9.name = "as1212121d";
            asd9.score = 100;

            List<IPoolData> asds = new List<IPoolData>();
            asds.Add(asd);
            asds.Add(asd2);
            asds.Add(asd3);
            asds.Add(asd4);
            asds.Add(asd5);
            asds.Add(asd6);
            asds.Add(asd7);asds.Add(asd7);
            asds.Add(asd8);
            asds.Add(asd9);asds.Add(asd8);
            asds.Add(asd9);asds.Add(asd8);
            asds.Add(asd9);asds.Add(asd8);
            asds.Add(asd9);asds.Add(asd9);asds.Add(asd9);asds.Add(asd9);asds.Add(asd7);asds.Add(asd7);asds.Add(asd7);asds.Add(asd7);

            _view.UpdateScrollView(asds);
        }
    }
}
