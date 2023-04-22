using System;
using Miniclip.Entities.Moles;
using UnityEngine;

namespace Miniclip.Game
{
    public class MoleController : MonoBehaviour
    {
        public MoleView _view;
        private bool _hasHelmet;
        private bool _hasBomb;
        public Action OnMoleDie;
        
        public void SetupMole(Mole mole, Sprite moleSprite)
        {
            gameObject.SetActive(true);
            _hasHelmet = mole.HasHelment();
            _hasBomb = mole.HasBomb();
            _view.EnableHelmet(_hasHelmet);
            _view.EnableBomb(_hasBomb);
            _view.SetSprite(moleSprite);
            _view.OnMoleClicked += OnHit;
        }

        private void OnHit()
        {
            if (_hasHelmet)
            {
                _hasHelmet = false;
                _view.EnableHelmet(_hasHelmet);
                return;
            }
            if (_hasBomb)
            {
                OnMoleDie();
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
