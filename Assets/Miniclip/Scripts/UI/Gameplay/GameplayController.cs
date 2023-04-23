using System;
using UnityEditor;
using UnityEngine;

namespace Miniclip.UI.Gameplay
{
    public class GameplayController : UIPanel
    {
        [SerializeField] private GameplayView _view;
        [SerializeField] private Timer.Timer _timer;

        public void Subscribe(Action UnpauseGame, Action LeaveGame)
        {
            _view.Subscribe(() =>
            {
                HidePauseMenu();
                UnpauseGame?.Invoke();
            }, () =>
            {
                LeaveGame?.Invoke();
            });
        }

        public void ShowStartingTimer(Action startingAnimationFinished)
        {
            _view.EnableStartingTimer(startingAnimationFinished);
        }

        public void StartTimerCountdown(int gameDataTimer, Action timerFinished)
        {
            _timer.InitTimer(gameDataTimer,timerFinished);
        }

        public void Pause()
        {
            _view.EnablePause(true);
        }
        
        private void HidePauseMenu()
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
    }

}
