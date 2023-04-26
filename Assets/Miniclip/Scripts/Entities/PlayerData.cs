using System;
using System.Collections.Generic;

namespace Miniclip.Entities
{
    [Serializable]
    public class PlayerData
    {
        public List<AttemptData> PlayerAttempts = new List<AttemptData>();

        public void AddAttempt(AttemptData data)
        {
            PlayerAttempts.Add(data);
        }
    }
    
    [Serializable]
    public class PlayerOptionsData
    {
        public bool ShowTutorial = true;
    }
}
