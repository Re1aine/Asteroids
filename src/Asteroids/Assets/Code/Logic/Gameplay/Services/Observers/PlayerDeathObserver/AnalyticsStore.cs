using UnityEngine;

public class AnalyticsStore : IAnalyticsStore
{
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

    public void DebugAnalytic()
    {
        Debug.Log($"BulletReleaseCount - {_bulletReleaseCount}");
        Debug.Log($"LaserReleaseCount - {_laserReleaseCount}");
        Debug.Log($"AsteroidKills - {_asteroidKills}");
        Debug.Log($"UfoKills - {_ufoKills}");
    }

    public void Flush()
    {
        _bulletReleaseCount = 0;
        _laserReleaseCount = 0;
        _asteroidKills = 0;
        _ufoKills = 0;
    }
}