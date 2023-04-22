namespace Miniclip.Entities.Moles
{
    public class NormalMole : Mole
    {
        public NormalMole()
        {
            RequiredHitsToDie = 1;
            ComboModifier = 1;
            Sprite = "normalMole";
        }
    }
}
