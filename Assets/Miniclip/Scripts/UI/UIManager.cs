using System;
using Miniclip.UI.MainMenu;
using UnityEngine;

namespace Miniclip.UI
{
    public enum Panel
    {
        MainMenu,
        Tutorial,
        Gameplay,
        Highscores
    }
    
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private RectTransform playfabErrorPanel;
        [SerializeField] private MainMenuController mainMenuController;
        
        private void Awake()
        {
            mainMenuController.Init(this);
        }

        public void SwitchPanel(Panel panel)
        {
            switch (panel)
            {
                case Panel.MainMenu:
                    // Close current panel
                    // Go to Main Menu
                    
                    break;
                case Panel.Tutorial:
                    // Close current panel
                    // Go to Tutorial

                    break;
                case Panel.Gameplay:
                    // Close current panel
                    // Go to Gameplay

                    break;
                case Panel.Highscores:
                    // Close current panel
                    // Go to Highscores

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(panel), panel, null);
            }
        }
        
        public void ShowLoadingScreen(bool enable)
        {
            
        }

        public void ShowPlayfabErrorMessage(bool enable)
        {
            
        }
    }
}