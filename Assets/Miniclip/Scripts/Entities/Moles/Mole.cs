using Miniclip.Game;
using UnityEngine;

namespace Miniclip.Entities.Moles
{
    public abstract class Mole : MonoBehaviour
    {
        protected int RequiredHitsToDie;
        protected int ComboModifier;
        protected string Sprite;

        public string GetSpriteName()
        {
            return Sprite;
        }

        public int GetRequiredHitsToDie()
        {
            return RequiredHitsToDie;
        }
        
        public int GetComboModifier()
        {
            return ComboModifier;
        }
    }
}
