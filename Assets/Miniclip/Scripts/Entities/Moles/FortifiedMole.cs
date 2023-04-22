namespace Miniclip.Entities.Moles
{
    public class FortifiedMole : Mole
    {
        public FortifiedMole()
        {
            RequiredHitsToDie = 2;
            ComboModifier = 1;
            Sprite = "fortifiedMole";
        }
    }
}
