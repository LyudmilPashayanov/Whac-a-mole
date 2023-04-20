public struct GameData
{
    private int timer;
    private int pointsPerHit;
    private int comboX2;
    private int comboX3;
    private int extraHitPoints;

    public GameData(int timer, int pointsPerHit, int comboX2, int comboX3, int extraHitPoints)
    {
        this.timer = timer;
        this.pointsPerHit = pointsPerHit;
        this.comboX2 = comboX2;
        this.comboX3 = comboX3;
        this.extraHitPoints = extraHitPoints;
    }
}
