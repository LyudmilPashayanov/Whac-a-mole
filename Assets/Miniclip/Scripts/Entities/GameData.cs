namespace Miniclip.Entities
{
    public struct GameData
    {
        public int Timer;
        public int PointsPerHit;
        public int ComboX2;
        public int ComboX3;
        public int ExtraHitPoints;

        public GameData(int timer, int pointsPerHit, int comboX2, int comboX3, int extraHitPoints)
        {
            Timer = timer;
            PointsPerHit = pointsPerHit;
            ComboX2 = comboX2;
            ComboX3 = comboX3;
            ExtraHitPoints = extraHitPoints;
        }
    }
}