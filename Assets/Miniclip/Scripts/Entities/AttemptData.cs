using System;
using Miniclip.Pooler;

namespace Miniclip.Entities
{
    [Serializable]
    public class AttemptData : IPoolData
    {
        public int Score { get; set; }
        public string Name { get; set; }
    }
}
