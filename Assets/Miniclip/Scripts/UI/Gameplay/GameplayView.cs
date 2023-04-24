using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Miniclip.UI.Gameplay
{
   public class GameplayView : MonoBehaviour
   {
      private const string END_TEXT = "Time's up!"; // Could later on be a key to a localized text.

      [SerializeField] private RectTransform _centeredTextField;
      [SerializeField] private GameObject _pauseTab;
      [SerializeField] private Button _pauseButton;
      [SerializeField] private Button _pauseTabContinueButton;
      [SerializeField] private Button _pauseTabMainMenuButton;
      [SerializeField] private TMP_Text _centeredText;
      [SerializeField] private RectTransform[] _spawnPoints;
      
      private Sequence _centeredTextSequence;
      
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
         _pauseButton.gameObject.SetActive(false);
         _centeredTextSequence.Kill();
         _centeredTextSequence = DOTween.Sequence();
         _centeredTextField.localScale = Vector3.zero;
         _centeredText.text = END_TEXT;
         _centeredTextField.gameObject.SetActive(true);
         _centeredTextSequence.Append(_centeredTextField.DOScale(Vector3.one, 0.7f).SetEase(Ease.OutBounce));
         _centeredTextSequence.AppendInterval(3).OnComplete(
            () =>
            {
               animationCompleted.Invoke();
            });
      }

      public void EnableStartingTimer(Action animationCompleted)
      {
         _centeredTextSequence.Kill();
         _centeredTextSequence = DOTween.Sequence();
         _centeredTextField.localScale = Vector3.zero;
         _centeredText.text = "3";
         _centeredTextField.gameObject.SetActive(true);
         _centeredTextSequence.Append(_centeredTextField.DOScale(Vector3.one, 0.8f));
         _centeredTextSequence.Append(_centeredTextField.DOScale(Vector3.zero, 0.8f).OnComplete(() =>
         {
            _centeredText.text = "2";
         }));
         _centeredTextSequence.Append(_centeredTextField.DOScale(Vector3.one, 0.8f));
         _centeredTextSequence.Append(_centeredTextField.DOScale(Vector3.zero, 0.8f).OnComplete(() =>
         {
            _centeredText.text = "1";
         }));
         _centeredTextSequence.Append(_centeredTextField.DOScale(Vector3.one, 0.8f));
         _centeredTextSequence.Append(_centeredTextField.DOScale(Vector3.zero, 0.8f).OnComplete(() =>
         {
            _centeredText.text = "GO!";
         }));
         _centeredTextSequence.Append(_centeredTextField.DOScale(Vector3.one, 0.8f));
         _centeredTextSequence.Append(_centeredTextField.DOScale(Vector3.zero, 0.8f));
         _centeredTextSequence.OnComplete(() =>
         {
            animationCompleted.Invoke();
         });
      }

      public void EnablePause(bool enable)
      {
         if (enable)
         {
            _pauseTab.SetActive(true);
            if (_centeredTextSequence.IsPlaying())
            {
               _centeredTextSequence.Pause();
            }
         }
         else
         {
            _pauseTab.SetActive(false);
            if (_centeredTextSequence.IsActive())
            {
               _centeredTextSequence.Play();
            }
         }
      }

      public void Reset()
      {
         EnablePause(false);
         _centeredTextSequence.Kill();
         _centeredTextField.gameObject.SetActive(false);
         _centeredText.text = "";
      }
   }
}
