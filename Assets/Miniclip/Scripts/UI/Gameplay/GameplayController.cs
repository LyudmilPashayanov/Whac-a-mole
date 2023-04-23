using System;
using UnityEditor;
using UnityEngine;

namespace Miniclip.UI.Gameplay
{
    public class GameplayController : UIPanel
    {
        [SerializeField] private GameplayView _view;
        [SerializeField] private Timer.Timer _timer;

        public void Subscribe(Action UnpauseGame, Action LeaveGame, Action PauseGame)
        {
            _view.Subscribe(() =>
            {
                UnpauseGame?.Invoke();
            }, () =>
            {
                LeaveGame?.Invoke();
            }, () =>
            {
                PauseGame?.Invoke();
            });
        }

        public void ShowStartingTimer(Action startingAnimationFinished)
        {
            _view.EnableStartingTimer(startingAnimationFinished);
        }

        public void StartTimerCountdown(int gameDataTimer, Action timerFinished)
        {
            _timer.InitTimer(gameDataTimer,timerFinished);
            _timer.StartTimer();
        }
        
        public void StopTimerCountdown()
        {
            _timer.StopTimer();
        }
        
        public void ResumeTimerCountdown()
        {
            _timer.StartTimer();
        }

        public void Pause()
        {
            _view.EnablePause(true);
        }
        
        public void HidePauseMenu()
        {
            _view.EnablePause(false);
        }

        public void FinishGame(Action endTextAnimationFinish)
        {
            _view.EnableEndTextAnimation(()=>
            {
                endTextAnimationFinish?.Invoke();
            });
        }

        protected override void OnViewLeft()
        {
            _view.Reset();
            _timer.Reset();
        }
    }

}
