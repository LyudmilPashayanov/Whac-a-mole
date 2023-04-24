using System;
using UnityEngine;

namespace Miniclip.Entities.Moles
{
    public abstract class Mole : MonoBehaviour
    {
        protected bool Helmet;
        protected bool Bomb;
        protected string Sprite;
        protected int Lives;

        public event Action OnMoleHit;
        public event Action OnMoleDied;
        public event Action OnMoleExploded;
        
        public string GetSpriteName()
        {
            return Sprite;
        }

        public bool HasHelmet()
        {
            return Helmet;
        }
        
        public bool HasBomb()
        {
            return Bomb;
        }
        
        public virtual void Hit()
        {
            OnMoleHit?.Invoke();
            Lives--;
            if (Lives == 0)
            {
                Die();
            }
        }
        
        protected virtual void Die()
        {
            OnMoleDied?.Invoke();
        }
        
        protected void BreakHelmet()
        {
            Helmet = false;
        }

        protected void Explode()
        {
            OnMoleExploded?.Invoke();
        }
    }
}
