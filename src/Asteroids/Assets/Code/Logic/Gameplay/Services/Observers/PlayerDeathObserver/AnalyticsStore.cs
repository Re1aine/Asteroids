
public class AnalyticsStore : IAnalyticsStore
{
    public int BulletReleaseCount => _bulletReleaseCount;
    public int LaserReleaseCount => _laserReleaseCount;
    public int AsteroidKills => _asteroidKills;
    public int UfoKills => _ufoKills;
    
    private int _bulletReleaseCount;
    private int _laserReleaseCount;
    private int _asteroidKills;
    private int _ufoKills;
    
    public void AddBullet() => 
        _bulletReleaseCount++;

    public void AddLaser() => 
        _laserReleaseCount++;

    public void AddAsteroid() => 
        _asteroidKills++;

    public void AddUfo() => 
        _ufoKills++;
    
    public void Flush()
    {
        _bulletReleaseCount = 0;
        _laserReleaseCount = 0;
        _asteroidKills = 0;
        _ufoKills = 0;
    }
}