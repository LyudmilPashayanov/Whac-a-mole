using System;
using System.Collections.Generic;

namespace Miniclip.Entities
{
    /// <summary>
    /// This class holds the attempts done worldwide.
    /// </summary>
    [Serializable]
    public class WorldsData 
    {
        public List<AttemptData> worldWideAttempts = new List<AttemptData>();
    }
}

