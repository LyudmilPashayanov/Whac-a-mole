namespace Miniclip.Entities
{
    public struct GameData
    {
        public int Timer;
        public int PointsPerHit;
        public int ComboX2;
        public int ComboX3;
        public int ConsecutiveHitsRequired;

        public GameData(int timer, int pointsPerHit, int comboX2, int comboX3, int consecutiveHitsRequired)
        {
            Timer = timer;
            PointsPerHit = pointsPerHit;
            ComboX2 = comboX2;
            ComboX3 = comboX3;
            ConsecutiveHitsRequired = consecutiveHitsRequired;
        }
    }
}