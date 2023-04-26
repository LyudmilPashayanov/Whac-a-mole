using System;
using Miniclip.Entities.Moles;
using UnityEngine;

namespace Miniclip.Game
{
    /// <summary>
    /// This class contains the logic of the Mole as a visual game object.
    /// </summary>
    public class MoleController : MonoBehaviour
    {
        #region Variables

        [SerializeField] private MoleView _view;
        [SerializeField] private RectTransform _rectTransform;
        
        [NonSerialized] public RectTransform SpawningPoint;

        private Mole _mole;
        private bool _moleExploding;
        private event Action<MoleController> OnMoleDespawned;

        #endregion

        #region Functionality

         /// <summary>
        /// This method allows us to Inject the MoleController with data and re-use already spawned game object moles.
        /// </summary>
        /// <param name="mole"></param>
        /// <param name="moleSprite"></param>
        public void InitMole(Mole mole, Sprite moleSprite)
        {
            _mole = mole;
            SubscribeOnHitEvent(MoleHit);
            SubscribeOnExplodeEvent(MoleExplode);
            SubscribeOnDieEvent(MoleDie);
            _view.SetSprite(moleSprite);
            UpdateMoleAppearance();
            ToggleInteractable(true);
            _view.OnMoleClicked += _mole.Hit;
            _view.OnMoleHidden += ResetMole;
        }

        public void SetupPosition(RectTransform spawningPosition)
        {
            SpawningPoint = spawningPosition;
            transform.SetParent(spawningPosition);
            _rectTransform.localScale = Vector3.zero;
            _rectTransform.anchoredPosition = Vector2.zero;
            gameObject.SetActive(true);
        }
        
        /// <summary>
        /// Sets the duration which the mole will stay visible on the screen before starting to hide.
        /// </summary>
        /// <param name="showSpeed"></param>
        public void SetShowSpeed(float showSpeed)
        {
            _view.SetShowSpeed(showSpeed);
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
            _view.EnableExplosionSprite(true);
            HideMole();
        }
        
        public void ShowMole(float hideAfterTime)
        {
            _view.ShowMole(hideAfterTime);
        }

        private void HideMole()
        {
            ToggleInteractable(false);
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

        public void ToggleInteractable(bool toggle)
        {
            _view.ToggleInteractable(toggle);
        }
        
        /// <summary>
        /// Resets the Mole GameObjects and makes it ready for re-use
        /// </summary>
        public void ResetMole()
        {
            _view.OnMoleClicked -= _mole.Hit;
            _view.OnMoleHidden -= ResetMole;
            OnMoleDespawned?.Invoke(this);
            _view.EnableExplosionSprite(false);
            gameObject.SetActive(false);
            _moleExploding = false;
            // Reset all the fields so that they can be REUSED
        }


        #endregion
       
        #region Event Handlers

        public void SubscribeOnDieEvent(Action<MoleType> moleDied)
        {
            _mole.OnMoleDied += moleDied;
        }

        public void SubscribeOnDespawnEvent(Action<MoleController> moleDespawned)
        {
            OnMoleDespawned += moleDespawned;
        }
        
        public void UnsubscribeOnDespawnEvent(Action<MoleController> moleDespawned)
        {
            OnMoleDespawned -= moleDespawned;
        }
        
        private void SubscribeOnHitEvent(Action moleHit)
        {
            _mole.OnMoleHit += moleHit;
        }
        
        private void SubscribeOnExplodeEvent(Action moleExploded)
        {
            _mole.OnMoleExploded += moleExploded;
        }

        #endregion
       
    }
}
