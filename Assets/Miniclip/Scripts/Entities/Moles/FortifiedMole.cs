using Miniclip.Game;

namespace Miniclip.Entities.Moles
{
    public class FortifiedMole : Mole
    {
        public FortifiedMole()
        {
            Bomb = false;
            Helmet = true;
            Sprite = "normalMole";
            Lives = 2;
            MoleType = MoleType.Fortified;
        }
        
        public override void Hit()
        {
            BreakHelmet();
            base.Hit();
        }
    }
}
