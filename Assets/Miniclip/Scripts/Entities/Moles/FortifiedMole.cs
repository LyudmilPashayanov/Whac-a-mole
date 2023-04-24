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
        }
        
        public override void Hit()
        {
            BreakHelmet();
            base.Hit();
        }
    }
}
