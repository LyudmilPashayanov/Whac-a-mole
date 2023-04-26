using System;
using Miniclip.Pooler;

namespace Miniclip.Entities
{
    [Serializable]
    public class AttemptData
    {
        public int Score { get; set; }
        public string Name { get; set; }
    }
    
    public class AttemptDataUI : IPoolData
    {
        public int Score { get; set; }
        public string Name { get; set; }
        
        public int Placement { get; set; }
        public bool Highlighted { get; set; }

        public AttemptDataUI(int score, string name, int placement,bool highlighted)
        {
            Score = score;
            Name = name;
            Placement = placement;
            Highlighted = highlighted;
        }
    }
}
