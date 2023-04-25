using System;
using Miniclip.Game;

namespace Miniclip.Entities.Moles
{
    public abstract class Mole
    {
        protected bool Helmet;
        protected bool Bomb;
        protected string Sprite;
        protected int Lives;
        protected MoleType MoleType;
        public event Action OnMoleHit;
        public event Action<MoleType> OnMoleDied;
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
            OnMoleDied?.Invoke(MoleType);
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
