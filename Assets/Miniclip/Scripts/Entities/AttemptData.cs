using System;

namespace Miniclip.Entities
{
    /// <summary>
    /// Simple class representing one attempt at Whac-A-Mole 
    /// </summary>
    [Serializable]
    public class AttemptData
    {
        public int Score { get; set; }
        public string Name { get; set; }
    }
}
