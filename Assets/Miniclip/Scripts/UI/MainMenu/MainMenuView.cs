using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Miniclip.UI.MainMenu
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private RectTransform _title;
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private Button _tutorialButton;
        [SerializeField] private Button _startGameButton;
        [SerializeField] private CanvasGroup _tutorialButtonCanvasGroup;
        
        private Tween _titleAnim;
        
        private void Start()
        {
            _inputField.onValueChanged.AddListener(CheckStartButtonActivation);
            _titleAnim = _title.DOScale(1.1f,0.5f).SetLoops(-1,LoopType.Yoyo);
        }

        private void CheckStartButtonActivation(string text)
        {
            if (text.Length > 0)
            {
                SetStartGameButtonInteractable(true);
                if ( /* show tutorial button */true)
                {
                    SetTutorialButtonInteractable(true);
                    ShowTutorialButton(true);
                }
            }
            else
            {
                SetTutorialButtonInteractable(false);
                ShowTutorialButton(false);
                SetStartGameButtonInteractable(false);
            }
        }
        
        public void Subscribe(Action tutorialButtonClicked, Action startGameButtonClicked)
        {
            _tutorialButton.onClick.AddListener(() => tutorialButtonClicked?.Invoke());
            _startGameButton.onClick.AddListener(() => startGameButtonClicked?.Invoke());
        }

        public string GetName()
        {
            return _inputField.text;
        }

        public void SetStartGameButtonInteractable(bool interactable)
        {
            _startGameButton.interactable = interactable;
        }

        public void EnableTextInput(bool enable)
        {
            _inputField.enabled = enable;
        }

        public void SetTutorialButtonInteractable(bool interactable)
        {
            _tutorialButton.interactable = interactable;
        }
        
        private void ShowTutorialButton(bool show)
        {
            if (show)
            {
                _tutorialButtonCanvasGroup.DOFade(1, 0.5f);
            }
            else
            {
                _tutorialButtonCanvasGroup.DOFade(0, 0.5f);
            }
        }

        public void StartTitleAnimation(bool play)
        {
            if (play)
            {
                _titleAnim.Play();
            }
            else
            {
                _titleAnim.Pause();
            }
        }

        public void Reset()
        {
            _inputField.text = "";
        }
    }
}
    
