using System;
using Miniclip.Entities.Moles;
using UnityEngine;

namespace Miniclip.Game
{
    public class MoleController : MonoBehaviour
    {
        public MoleView _view;

        private int _requiredHits;
        private MoleType _moleType;

        public Action OnMoleDie;
        public void SetupMole(Mole mole, MoleType type)
        {
            _requiredHits = mole.GetRequiredHitsToDie();
            _moleType = type;
            //Sprite spriteToSet = mole.GetSprite();
            //_view.SetSprite(spriteToSet);
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
            // Reset all the fields so that they can be REUSED
        }

        public MoleType GetMoleType()
        {
            return _moleType;
        }
    }
}
