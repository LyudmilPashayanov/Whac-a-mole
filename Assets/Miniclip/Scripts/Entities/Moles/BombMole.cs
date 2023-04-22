namespace Miniclip.Entities.Moles
{
    public class BombMole : Mole
    {
        public BombMole()
        {
            RequiredHitsToDie = 1;
            ComboModifier = -1;
            Sprite = "bombMole";
        }
    }
}
