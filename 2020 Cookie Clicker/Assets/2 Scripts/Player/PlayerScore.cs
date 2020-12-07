public class PlayerScore
{
    private int pointsPerClick = 1;
    private int pointsPerSecond = 0;

    public int PointsPerClick() { return pointsPerClick; }
    public int PointsPerSecond() { return pointsPerSecond; }
    public void SetPointsPerClick(int ppc)
    {
        pointsPerClick += ppc;
    }
    public void SetPointsPerSecond(int pps)
    {
        pointsPerSecond += pps;
    }
}
