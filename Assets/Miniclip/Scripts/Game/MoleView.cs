using System;
using UnityEngine;

public class MoleView : MonoBehaviour
{
    [SerializeField] private Sprite _sprite;

    public event Action OnMoleClicked;


    public void SetSprite(Sprite sprite)
    {
        _sprite = sprite;
    }
    
    private void OnMouseDown()
    {
        if (OnMoleClicked != null)
        {
            OnMoleClicked();
        }
    }
    
}
