using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Miniclip.UI.Gameplay
{
   /// <summary>
   /// Handles all UI related logic in the Gameplay Panel. 
   /// </summary>
   public class GameplayView : MonoBehaviour
   {
      #region Variables

      private const string END_TEXT = "Time's up!"; // Could later on be a key to a localized text.
      private const float STARTING_TIMER_DURATION = 0.6f;

      [SerializeField] private RectTransform _centeredTextField;
      [SerializeField] private GameObject _pauseTab;
      [SerializeField] private Button _pauseButton;
      [SerializeField] private Button _pauseTabContinueButton;
      [SerializeField] private Button _pauseTabMainMenuButton;
      [SerializeField] private TMP_Text _centeredText;
      [SerializeField] private List<RectTransform> _spawnPoints;
      [SerializeField] private TMP_Text _scoreText;
      [SerializeField] private TMP_Text _comboText;

      private Sequence _startingTimerSequence;
      private Sequence _endTextSequence;
      
      #endregion

      #region Functionality

       public void Subscribe(Action continueGameplay, Action goToMainMenu, Action pauseGame)
      {
         _pauseTabMainMenuButton.onClick.AddListener(()=>goToMainMenu?.Invoke());
         _pauseTabContinueButton.onClick.AddListener(()=>continueGameplay?.Invoke());
         _pauseButton.onClick.AddListener(()=>pauseGame?.Invoke());
      }

      public RectTransform GetSpawnPoint(int index)
      {
         return _spawnPoints[index];
      }
      
      public void EnableEndTextAnimation(Action animationCompleted)
      { 
         _centeredTextField.localScale = Vector3.zero;
         _centeredText.text = END_TEXT;
         _centeredTextField.gameObject.SetActive(true);
         
         _endTextSequence = DOTween.Sequence();
         _endTextSequence.SetAutoKill(false);
         _endTextSequence.Append(_centeredTextField.DOScale(Vector3.one, 0.7f).SetEase(Ease.OutBounce));
         _endTextSequence.AppendInterval(2);
         _endTextSequence.OnComplete(() =>
            {
               animationCompleted?.Invoke();
            });
      }

      public void EnableStartingTimer(Action animationCompleted)
      {
         
         _centeredTextField.localScale = Vector3.zero;
         _centeredText.text = "3";
         _centeredTextField.gameObject.SetActive(true);
         
         _startingTimerSequence = DOTween.Sequence();
         _startingTimerSequence.Append(_centeredTextField.DOScale(Vector3.one, STARTING_TIMER_DURATION));
         _startingTimerSequence.Append(_centeredTextField.DOScale(Vector3.zero, STARTING_TIMER_DURATION).OnComplete(() =>
         {
            _centeredText.text = "2";
         }));
         _startingTimerSequence.Append(_centeredTextField.DOScale(Vector3.one, STARTING_TIMER_DURATION));
         _startingTimerSequence.Append(_centeredTextField.DOScale(Vector3.zero, STARTING_TIMER_DURATION).OnComplete(() =>
         {
            _centeredText.text = "1";
         }));
         _startingTimerSequence.Append(_centeredTextField.DOScale(Vector3.one, STARTING_TIMER_DURATION));
         _startingTimerSequence.Append(_centeredTextField.DOScale(Vector3.zero, STARTING_TIMER_DURATION).OnComplete(() =>
         {
            _centeredText.text = "GO!";
         }));
         _startingTimerSequence.Append(_centeredTextField.DOScale(Vector3.one, STARTING_TIMER_DURATION));
         _startingTimerSequence.Append(_centeredTextField.DOScale(Vector3.zero, STARTING_TIMER_DURATION));
         _startingTimerSequence.OnComplete(() =>
         {
     
            animationCompleted.Invoke();
         });
      }

      public void EnablePause(bool enable)
      {
         if (enable)
         {
            _pauseTab.SetActive(true);
            _startingTimerSequence.Pause();
         }
         else
         {
            _pauseTab.SetActive(false);
            _startingTimerSequence.PlayForward();
         }
      }

      public void Reset()
      {
         EnablePause(false);
         _startingTimerSequence.Kill();
         _centeredTextField.gameObject.SetActive(false);
         _centeredText.text = "";
         UpdateComboText(1);
         UpdateScoreText(0);
      }

      public void UpdateComboText(int combo)
      {
         _comboText.text = combo.ToString();
      }

      public void UpdateScoreText(int score)
      {
         _scoreText.text = score.ToString();
      }

      public int GetSpawnPointIndex(RectTransform spawnPoint)
      {
         return _spawnPoints.IndexOf(spawnPoint);
      }

      public void EnablePauseButton(bool enable)
      {
         _pauseButton.interactable = enable;
      }

      #endregion
     
   }
}
