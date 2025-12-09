using Code.Logic.Gameplay.Analytics;
using Code.Logic.Gameplay.Analytics.AnalyticsStore;
using Code.Logic.Gameplay.Services.Providers.PlayerProvider;

public class PlayerGunObserver : IPlayerGunObserver
{
    private readonly IPlayerProvider _playerProvider;
    private readonly IAnalytics _analytics;
    private readonly IAnalyticsStore _analyticsStore;
    private readonly IAudioService _audioService;
    
    public PlayerGunObserver(IPlayerProvider playerProvider,
        IAnalytics analytics,
        IAnalyticsStore analyticStore,
        IAudioService audioService)
    {
        _playerProvider = playerProvider;
        _analytics = analytics;
        _analyticsStore = analyticStore;
        _audioService = audioService;
    }

    public void Start()
    {
        _playerProvider.Player.View.Gun.BulletShoot += _analyticsStore.AddBullet;
        _playerProvider.Player.View.Gun.LaserShootStarted += _analyticsStore.AddLaser;

        _playerProvider.Player.View.Gun.LaserShootStarted += _analytics.SendLaserUsedEvent;
        
        _playerProvider.Player.View.Gun.BulletShoot += OnBulletShoot;
        _playerProvider.Player.View.Gun.LaserShootStarted += OnLaserShootStarted;
        _playerProvider.Player.View.Gun.LaserShootEnded += OnLaserEnded;
    }

    public void Stop()
    {
        _playerProvider.Player.View.Gun.BulletShoot -= _analyticsStore.AddBullet;
        _playerProvider.Player.View.Gun.LaserShootStarted  -= _analyticsStore.AddLaser;
        
        _playerProvider.Player.View.Gun.LaserShootStarted -= _analytics.SendLaserUsedEvent;
        
        _playerProvider.Player.View.Gun.BulletShoot -= OnBulletShoot;
        _playerProvider.Player.View.Gun.LaserShootStarted -= OnLaserShootStarted;
        _playerProvider.Player.View.Gun.LaserShootEnded -=  OnLaserEnded;
    }

    private void OnBulletShoot() => 
        _audioService.PlayShortSound(SFXType.BulletShoot);

    private void OnLaserShootStarted() => 
        _audioService.PlayShortSound(SFXType.LaserShoot);

    private void OnLaserEnded() => 
        _audioService.StopShortSound(SFXType.LaserShoot);
}