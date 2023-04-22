using System;
using UnityEngine;
using UnityEngine.UI;

public class MoleView : MonoBehaviour
{
    [SerializeField] private Image _moleImage;
    [SerializeField] private RectTransform _bomb;
    [SerializeField] private RectTransform _helmet;
    
    public event Action OnMoleClicked;


    public void EnableBomb(bool enable)
    {
        _bomb.gameObject.SetActive(enable);
    }
    
    public void EnableHelmet(bool enable)
    {
        _helmet.gameObject.SetActive(enable);
    }
    
    public void SetSprite(Sprite sprite)
    {
        _moleImage.sprite = sprite;
    }
    
    private void OnMouseDown()
    {
        if (OnMoleClicked != null)
        {
            OnMoleClicked();
        }
    }
    
}
