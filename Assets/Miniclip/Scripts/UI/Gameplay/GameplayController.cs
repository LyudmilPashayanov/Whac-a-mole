using System;
using Miniclip.Game;
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

        public RectTransform GetSpawningPosition(int spawnPointIndex)
        {
            return _view.GetSpawnPoint(spawnPointIndex);
        }
        
        public int GetSpawningPointIndex(RectTransform spawnPoint)
        {
            return _view.GetSpawnPointIndex(spawnPoint);
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

        public void EnablePauseButton(bool enable)
        {
            _view.EnablePauseButton(enable);
        }
        
        public void FinishGame(Action endTextAnimationFinish)
        {
            EnablePauseButton(false);
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

        public void UpdateScore(ScoreData scoreData)
        {
            _view.UpdateScoreText(scoreData.Hits);
            _view.UpdateComboText(scoreData.Combo);
        }
    }

}
