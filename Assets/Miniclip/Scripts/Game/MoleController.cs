using System;
using Miniclip.Entities.Moles;
using UnityEngine;

namespace Miniclip.Game
{
    public class MoleController : MonoBehaviour
    {
        private MoleView _view;
        private Mole _mole;
        private bool _moleExploding;
        
        public void SetupMole(Mole mole, Sprite moleSprite)
        {
            gameObject.SetActive(true);
            _mole = mole;
            SubscribeOnHitEvent(MoleHit);
            SubscribeOnExplodeEvent(MoleExplode);
            SubscribeOnDiedEvent(MoleDie);
            _view.SetSprite(moleSprite);
            UpdateMoleAppearance();
            _view.OnMoleClicked += _mole.Hit;
        }

        public void SubscribeOnHitEvent(Action moleHit)
        {
            _mole.OnMoleHit += moleHit;
        }
        
        public void SubscribeOnDiedEvent(Action<MoleType> moleDied)
        {
            _mole.OnMoleDied += moleDied;
        }
        
        public void SubscribeOnExplodeEvent(Action moleExploded)
        {
            _mole.OnMoleExploded += moleExploded;
        }
        
        private void UpdateMoleAppearance()
        {
            _view.EnableHelmet(_mole.HasHelmet());
            _view.EnableBomb(_mole.HasBomb());
        }

        private void MoleHit()
        {
            UpdateMoleAppearance();
        }
        
        private void MoleDie(MoleType _)
        {
            if (_moleExploding)
            {
                return;
            }
             
            ResetMole();
        }

        private void MoleExplode()
        {
            _moleExploding = true;
             _view.StartExplosion(ResetMole);
        }
        
        public void ResetMole()
        {
            _view.OnMoleClicked -= _mole.Hit;
            _view.DestroyMole();
            _moleExploding = false;
            // Reset all the fields so that they can be REUSED
        }
    }
}
