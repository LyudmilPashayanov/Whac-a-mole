using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Miniclip.UI.MainMenu
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private Button _tutorialButton;
        [SerializeField] private Button _startGameButton;
        private void Start()
        {
            _inputField.onValueChanged.AddListener(CheckStartButtonActivation);
        }

        private void CheckStartButtonActivation(string text)
        {
            if (text.Length > 0)
            {
                SetStartGameButtonInteractable(true);
            }
            else
            {
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
    }
}
    
