using Miniclip.Game;
using UnityEngine;

namespace Miniclip.Entities.Moles
{
    public abstract class Mole : MonoBehaviour
    {
        protected bool Helmet;
        protected bool Bomb;
        protected string Sprite;

        public string GetSpriteName()
        {
            return Sprite;
        }

        public bool HasHelment()
        {
            return Helmet;
        }
        
        public bool HasBomb()
        {
            return Bomb;
        }
    }
}
