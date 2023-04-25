using System;
using Miniclip.Entities.Moles;
using UnityEngine;

namespace Miniclip.Game
{
    public class MoleController : MonoBehaviour
    {
        [SerializeField] private MoleView _view;
        [SerializeField] private RectTransform _rectTransform;
        private Mole _mole;
        private bool _moleExploding;
        private RectTransform _spawningPoint;
        private event Action<RectTransform> OnMoleDespawned;

        public void SetupMole(Mole mole, Sprite moleSprite)
        {
            gameObject.SetActive(true);
            _mole = mole;
            SubscribeOnHitEvent(MoleHit);
            SubscribeOnExplodeEvent(MoleExplode);
            SubscribeOnDieEvent(MoleDie);
            _view.SetSprite(moleSprite);
            UpdateMoleAppearance();
            _view.OnMoleClicked += _mole.Hit;
            _view.OnMoleHidden += ResetMole;
        }

        public void SubscribeOnHitEvent(Action moleHit)
        {
            _mole.OnMoleHit += moleHit;
        }
        
        public void SubscribeOnDieEvent(Action<MoleType> moleDied)
        {
            _mole.OnMoleDied += moleDied;
        }
        
        public void SubscribeOnExplodeEvent(Action moleExploded)
        {
            _mole.OnMoleExploded += moleExploded;
        }
        
        public void SubscribeOnDespawnEvent(Action<RectTransform> moleDespawned)
        {
            OnMoleDespawned += moleDespawned;
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
             
            HideMole();
        }

        private void MoleExplode()
        {
            _moleExploding = true;
             _view.StartExplosion(ResetMole);
        }
        
        private void ResetMole()
        {
            _view.OnMoleClicked -= _mole.Hit;
            _moleExploding = false;
            gameObject.SetActive(false);
            OnMoleDespawned?.Invoke(_spawningPoint);
            // Reset all the fields so that they can be REUSED
        }

        public void ShowMole(float hideAfterTime)
        {
            _view.ShowMole(hideAfterTime);
        }

        public void HideMole()
        {
            _view.HideMole();
        }

        public void SetupPosition(RectTransform spawningPosition)
        {
            _spawningPoint = spawningPosition;
            transform.SetParent(spawningPosition);
            _rectTransform.localScale = Vector3.zero;
            _rectTransform.anchoredPosition = Vector2.zero;
        }
    }
}
