using System;
using Miniclip.Entities.Moles;
using UnityEngine;

namespace Miniclip.Game
{
    public class MoleController : MonoBehaviour
    {
        [SerializeField] private MoleView _view;
        [SerializeField] private RectTransform _rectTransform;
        
        [NonSerialized] public RectTransform SpawningPoint;

        private Mole _mole;
        private bool _moleExploding;
        private event Action<MoleController> OnMoleDespawned;

        public void InitMole(Mole mole, Sprite moleSprite)
        {
            _mole = mole;
            SubscribeOnHitEvent(MoleHit);
            SubscribeOnExplodeEvent(MoleExplode);
            SubscribeOnDieEvent(MoleDie);
            _view.SetSprite(moleSprite);
            UpdateMoleAppearance();
            _view.EnableInteractable(true);
            _view.OnMoleClicked += _mole.Hit;
            _view.OnMoleHidden += ResetMole;
        }

        private void UpdateMoleAppearance()
        {
            _view.EnableHelmet(_mole.HasHelmet());
            _view.EnableBomb(_mole.HasBomb());
        }

        public void SetupPosition(RectTransform spawningPosition)
        {
            SpawningPoint = spawningPosition;
            transform.SetParent(spawningPosition);
            _rectTransform.localScale = Vector3.zero;
            _rectTransform.anchoredPosition = Vector2.zero;
            gameObject.SetActive(true);
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
        
        public void ShowMole(float hideAfterTime)
        {
            _view.ShowMole(hideAfterTime);
        }

        private void HideMole()
        {
            _view.HideMole();
        }

        public void PauseMole()
        {
            _view.PauseMole();
        }
        
        public void UnpauseMole()
        {
            _view.UnpauseMole();
        }

        public void DisableInteractable()
        {
            _view.EnableInteractable(false);
        }
        
        private void ResetMole()
        {
            _view.OnMoleClicked -= _mole.Hit;
            _view.OnMoleHidden -= ResetMole;
            Debug.Log("ResetMole");
            OnMoleDespawned?.Invoke(this);
            gameObject.SetActive(false);
            _moleExploding = false;
            // Reset all the fields so that they can be REUSED
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
        
        public void SubscribeOnDespawnEvent(Action<MoleController> moleDespawned)
        {
            OnMoleDespawned += moleDespawned;
        }
        
        public void UnsubscribeOnDespawnEvent(Action<MoleController> moleDespawned)
        {
            OnMoleDespawned -= moleDespawned;
        }
    }
}
