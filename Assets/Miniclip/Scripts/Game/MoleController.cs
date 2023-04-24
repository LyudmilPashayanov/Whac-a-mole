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
            _mole.OnMoleDied += MoleDie;
            _mole.OnMoleExploded += MoleExplode;
            _mole.OnMoleHit += MoleHit;
            _view.SetSprite(moleSprite);
            UpdateMoleAppearance();
            _view.OnMoleClicked += _mole.Hit;
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
        
        private void MoleDie()
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
