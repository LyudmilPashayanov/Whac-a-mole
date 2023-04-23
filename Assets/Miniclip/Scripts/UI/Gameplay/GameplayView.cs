using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Miniclip.UI.Gameplay
{
   public class GameplayView : MonoBehaviour
   {
      private const string END_TEXT = "Time's up!";

      [SerializeField] private RectTransform _centeredTextField;
      [SerializeField] private GameObject _pauseTab;
      [SerializeField] private Button _pauseTabContinueButton;
      [SerializeField] private Button _pauseTabMainMenuButton;
      [SerializeField] private TMP_Text _centeredText;
      private Sequence _centeredTextAnimation;
      
      public void Subscribe(Action ContinueGameplay, Action GoToMainMenu)
      {
         _pauseTabMainMenuButton.onClick.AddListener(GoToMainMenu.Invoke);
         _pauseTabContinueButton.onClick.AddListener(ContinueGameplay.Invoke);
      }  

      public void EnableEndTextAnimation(Action animationCompleted)
      { _centeredTextAnimation.Kill();
         _centeredTextAnimation = DOTween.Sequence();
         _centeredTextField.localScale = Vector3.zero;
         _centeredText.text = END_TEXT;
         _centeredTextField.gameObject.SetActive(true);
         _centeredTextAnimation.Append(_centeredTextField.DOScale(Vector3.one, 1.5f).SetEase(Ease.OutBounce));
         _centeredTextAnimation.AppendInterval(3).OnComplete(
            () =>
            {
               /*_centeredText.DOFade(0, 1.5f).OnComplete(() =>
               {*/
                  animationCompleted.Invoke();
             //  });
            });
      }

      public void EnableStartingTimer(Action animationCompleted)
      {
         _centeredTextAnimation.Kill();
         _centeredTextAnimation = DOTween.Sequence();
         _centeredTextField.localScale = Vector3.zero;
         _centeredText.text = "3";
         _centeredTextField.gameObject.SetActive(true);
         _centeredTextAnimation.Append(_centeredTextField.DOScale(Vector3.one, 0.8f));
         _centeredTextAnimation.Append(_centeredTextField.DOScale(Vector3.zero, 0.8f)).OnComplete(()=>
         {
            _centeredText.text = "2";
         });
         _centeredTextAnimation.Append(_centeredTextField.DOScale(Vector3.one, 0.8f));
         _centeredTextAnimation.Append(_centeredTextField.DOScale(Vector3.zero, 0.8f)).OnComplete(() =>
         {
            _centeredText.text = "1";
         });
         _centeredTextAnimation.Append(_centeredTextField.DOScale(Vector3.one, 0.8f));
         _centeredTextAnimation.Append(_centeredTextField.DOScale(Vector3.zero, 0.8f)).OnComplete(() =>
         {
            _centeredText.text = "GO!";
         });
         _centeredTextAnimation.Append(_centeredTextField.DOScale(Vector3.one, 0.8f));
         _centeredTextAnimation.Append(_centeredTextField.DOScale(Vector3.zero, 0.8f));
         _centeredTextAnimation.AppendInterval(3).OnComplete(() =>
         {
            animationCompleted.Invoke();
         });
      }

      public void EnablePause(bool enable)
      {
         if (enable)
         {
            _pauseTab.SetActive(true);
         }
         else
         {
            _pauseTab.SetActive(false);
         }
      }
   }
}
