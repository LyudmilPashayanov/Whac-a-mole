using System;
using TMPro;
using UnityEngine;

namespace Miniclip.UI.Timer
{
    public class Timer : MonoBehaviour
    {
       
        [SerializeField] private TMP_Text _timeText;
        
        private float _timer;
        private Action _timeFinished;
        private bool _timerStopped;
        private int _shownTime;
        void Update()
        {
            if(_timerStopped)
                return;
            
            _timer -= Time.deltaTime;

            UpdateText();
            
            if (_timer <= 0)
            {
                TimesUp();
            }
        }
        
        public void InitTimer(float time, Action timeFinished)
        {
            _timer = time;
            _timeFinished = timeFinished;
        }
        
        public void StartTimer()
        {
            _timerStopped = false;
        }
        
        public void StopTimer() 
        {
            _timerStopped = true;
        }

        private void UpdateText()
        {
            int tempTime = (int)_timer;
            if (_shownTime == tempTime)
            {
                return;
            }
            _shownTime = tempTime;
            _timeText.text = _shownTime.ToString();
        }

        private void TimesUp()
        {
            Debug.Log("Time's up!");
            StopTimer();
            _timeFinished?.Invoke();
            _timeFinished = null;
        }
    }
}
