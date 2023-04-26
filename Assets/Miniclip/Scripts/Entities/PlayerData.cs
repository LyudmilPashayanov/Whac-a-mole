using System;
using System.Collections.Generic;

namespace Miniclip.Entities
{
    /// <summary>
    /// This class represents all the attempts done on this device.
    /// </summary>
    [Serializable]
    public class PlayerData
    {
        public List<AttemptData> PlayerAttempts = new List<AttemptData>();

        public void AddAttempt(AttemptData data)
        {
            PlayerAttempts.Add(data);
        }
        
        public bool IsAttemptRecord(AttemptData attempt)
        {
            List<AttemptData> shallowSortedData = PlayerAttempts.GetRange(0, PlayerAttempts.Count);
            shallowSortedData.Sort( (a,b) => b.Score.CompareTo(a.Score));
            int highestScore = shallowSortedData[0].Score;
            if (highestScore <= attempt.Score)
            {
                return true;
            }

            return false;
        }
    }
    
    /// <summary>
    /// This class holds player options. They are saved to and retrieved from PlayFab.
    /// </summary>
    [Serializable]
    public class PlayerOptionsData
    {
        public bool ShowTutorial = true;
    }
}
