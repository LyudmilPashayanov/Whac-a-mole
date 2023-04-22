using System;
using UnityEngine;

namespace Miniclip.UI.Gameplay
{
    public class GameplayController : UIPanel
    {
        [SerializeField] private Timer.Timer _timer;
        
        public void ShowStartingTimer(int time,Action timerFinished)
        {
            // Timer before the starting spawning moles 3,2,1...
            timerFinished?.Invoke();
        }

        public void StartTimerCountdown(int gameDataTimer, Action timerFinished)
        {
            _timer.InitTimer(gameDataTimer,timerFinished);
        }
    }

}
