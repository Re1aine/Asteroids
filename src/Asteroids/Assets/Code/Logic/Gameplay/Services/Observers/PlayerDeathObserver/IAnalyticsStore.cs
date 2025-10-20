public interface IAnalyticsStore
{
    int BulletReleaseCount { get; }
    int LaserReleaseCount  { get; }
    int AsteroidKills { get; }
    int UfoKills { get; }
    void AddBullet();
    void AddLaser();
    void AddAsteroid();
    void AddUfo();
    void Flush();
}