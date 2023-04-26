using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Miniclip.UI.MainMenu
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private RectTransform _title;
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private Button _tutorialButton;
        [SerializeField] private Button _mainGameButton;
        [SerializeField] private TMP_Text _mainGameButtonText;
        [SerializeField] private CanvasGroup _tutorialButtonCanvasGroup;
        
        private Tween _titleAnim;
        
        private void Start()
        {
            _titleAnim = _title.DOScale(1.1f,0.5f).SetLoops(-1,LoopType.Yoyo);
        }

        public void Subscribe(Action tutorialButtonClicked, Action<string> inputValueChanged)
        {
            _tutorialButton.onClick.AddListener(() => tutorialButtonClicked?.Invoke());
            _inputField.onValueChanged.AddListener((text)=>
            {
                inputValueChanged?.Invoke(text);
            });
        }

        public string GetName()
        {
            return _inputField.text;
        }

        public void SetMainGameButtonInteractable(bool interactable)
        {
            _mainGameButton.interactable = interactable;
        }

        public void EnableTextInput(bool enable)
        {
            _inputField.enabled = enable;
        }

        public void SetTutorialButtonInteractable(bool interactable)
        {
            _tutorialButton.interactable = interactable;
        }
        
        public void ShowTutorialButton(bool show)
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

        public void SetMainButtonSettings(string text, Action onClickFunctionality)
        {
            _mainGameButton.onClick.RemoveAllListeners();
            _mainGameButton.onClick.AddListener(()=>
            {
                onClickFunctionality?.Invoke();
            });
            _mainGameButtonText.text = text;
        }
    }
}
    
