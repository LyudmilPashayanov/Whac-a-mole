using Miniclip.Game;

namespace Miniclip.Entities.Moles
{
    public class NormalMole : Mole
    {
        public NormalMole()
        {
            Bomb = false;
            Helmet = false;
            Sprite = "normalMole";
            Lives = 1;
            MoleType = MoleType.Normal;
        }
    }
}
