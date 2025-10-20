public interface IAnalyticsStore
{
    void AddBullet();
    void AddLaser();
    void AddAsteroid();
    void AddUfo();
    void DebugAnalytic();
    void Flush();
}