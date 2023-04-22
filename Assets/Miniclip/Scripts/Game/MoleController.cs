using System;
using Miniclip.Entities.Moles;
using UnityEngine;

namespace Miniclip.Game
{
    public class MoleController : MonoBehaviour
    {
        public MoleView _view;

        private int _requiredHits;

        public Action OnMoleDie;
    
    
        public void SetupMole(Mole mole)
        {
            _requiredHits = mole.GetRequiredHitsToDie();
            //Sprite spriteToSet = mole.GetSprite();
            //_view.SetSprite(spriteToSet);
            _view.OnMoleClicked += OnHit;
        }
    
        private void OnHit()
        {
            _requiredHits--;
     
            if (_requiredHits == 0)
            {
                
            }
        }

        public void MoleDie()
        {
            OnMoleDie?.Invoke();
        }
    
    }
}
