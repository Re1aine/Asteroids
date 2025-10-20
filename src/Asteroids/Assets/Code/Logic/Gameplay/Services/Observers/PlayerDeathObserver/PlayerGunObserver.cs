using Code.Logic.Gameplay.Services.Providers.PlayerProvider;

public class PlayerGunObserver : IPlayerGunObserver
{
    private readonly IPlayerProvider _playerProvider;
    private readonly IAnalytics _analytics;
    private readonly IAnalyticsStore _analyticsStore;

    public PlayerGunObserver(IPlayerProvider playerProvider, IAnalytics analytics, IAnalyticsStore analyticStore)
    {
        _playerProvider = playerProvider;
        _analytics = analytics;
        _analyticsStore = analyticStore;
    }

    public void Start()
    {
        _playerProvider.Player.View.Gun.BulletShoot += _analyticsStore.AddBullet;
        _playerProvider.Player.View.Gun.LaserShoot += _analyticsStore.AddLaser;

        _playerProvider.Player.View.Gun.LaserShoot += _analytics.SendLaserUsedEvent;
    }

    public void Stop()
    {
        _playerProvider.Player.View.Gun.BulletShoot -= _analyticsStore.AddBullet;
        _playerProvider.Player.View.Gun.LaserShoot  -= _analyticsStore.AddLaser;
        
        _playerProvider.Player.View.Gun.LaserShoot -= _analytics.SendLaserUsedEvent;
    }
}