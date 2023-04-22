using System;

namespace Miniclip.UI.Gameplay
{
    public class GameplayController : UIPanel
    {
        public void ShowStartingTimer(int time,Action timerFinished)
        {
            // Timer before the starting spawning moles 3,2,1...
            timerFinished?.Invoke();
        }

        public void StartTimerCountdown(int gameDataTimer, Action timerFinished)
        {
            // Timer logic 60,59,58....
            timerFinished?.Invoke();
        }
    }

}
