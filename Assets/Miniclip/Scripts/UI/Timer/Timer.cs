using System;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;

namespace Miniclip.UI.Timer
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _timeText;
        
        private float _timer;
        private Action _timeFinished;
        private bool _timerStopped = true;
        private int _shownTime;
        
        void Update()
        {
            if(_timerStopped)
                return;
            
            _timer -= Time.deltaTime;

            UpdateText();
            
            if (_shownTime == 0)
            {
                TimesUp();
            }
        }
        
        public void InitTimer(float time, Action timeFinished)
        {
            Debug.Log("time: " + time);
            _timer = time;
            _timeFinished = timeFinished;
            _timeText.text = ((int)time).ToString();
            _timeText.gameObject.SetActive(true);

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
            StopTimer();
            _timeFinished?.Invoke();
            _timeFinished = null;
        }

        public void Reset()
        {
            _timeFinished = null;
            _timerStopped = true;
            _timeText.gameObject.SetActive(false);
        }
    }
}
