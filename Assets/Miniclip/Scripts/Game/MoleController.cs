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
        
        public void SetupMole(Mole mole, Sprite moleSprite)
        {
            gameObject.SetActive(true);
            _requiredHits = mole.GetRequiredHitsToDie();
            _view.SetSprite(moleSprite);
            _view.OnMoleClicked += OnHit;
        }
    
        private void OnHit()
        {
            _requiredHits--;
            if (_requiredHits == 0)
            {
                MoleDie();
            }
        }

        private void MoleDie()
        {
            OnMoleDie?.Invoke();
        }

        public void ResetMole()
        {
            gameObject.SetActive(false);
            OnMoleDie = null;
            // Reset all the fields so that they can be REUSED
        }
    }
}
