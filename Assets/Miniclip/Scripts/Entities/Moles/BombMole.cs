namespace Miniclip.Entities.Moles
{
    public class BombMole : Mole
    {
        public BombMole()
        {
            Bomb = true;
            Helmet = false;
            Sprite = "stupidMole";
            Lives = 1;
        }

        protected override void Die()
        {
            Explode();
            base.Die();
        }
    }
}
