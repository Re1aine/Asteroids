using UnityEngine;

public class GameFactory : IGameFactory
{
    private readonly IScoreCountService _scoreCountService;
    private readonly IUFOsHolder _ufOsHolder;
    private readonly IAsteroidsHolder _asteroidsHolder;
    private readonly IBulletsHolder _bulletsHolder;
    private readonly IAssetsProvider _assetsProvider;
    
    public GameFactory(IScoreCountService scoreCountService,
        IUFOsHolder ufOsHolder,
        IAsteroidsHolder asteroidsHolder,
        IBulletsHolder bulletsHolder,
        IAssetsProvider assetsProvider)
    {
        _scoreCountService = scoreCountService;
        _ufOsHolder = ufOsHolder;
        _asteroidsHolder = asteroidsHolder;
        _bulletsHolder = bulletsHolder;
        _assetsProvider = assetsProvider;
    }

    public PlayerPresenter CreatePlayer(Vector3 position, Quaternion rotation)
    {
        PlayerView playerView = _assetsProvider.Instantiate<PlayerView>(AssetPath.Player);
        PlayerPresenter presenter = new PlayerPresenter(new PlayerModel(1), playerView);

        presenter.Init(new PlayerDamageReceiver(presenter), new PlayerDestroyer(presenter));
        
        return presenter;
    }

    public AsteroidPresenter CreateAsteroid(Vector3 position, Quaternion rotation, AsteroidType asteroidType, int scoreReward)
    {
        AsteroidView asteroidView = _assetsProvider.InstantiateAt<AsteroidView>(AssetPath.GetPathForAsteroid(asteroidType), position, rotation);
        
        AsteroidPresenter presenter = new AsteroidPresenter(new AsteroidModel(asteroidType, scoreReward), asteroidView);

        presenter.Init(new AsteroidDamageReceiver(presenter), new AsteroidDestroyer(presenter, this, _scoreCountService));

        _asteroidsHolder.Add(presenter);
        
        return presenter;
    }

    public UFOPresenter CreateUfo(Vector3 position, Quaternion rotation, int scoreReward)
    {
        UFOView ufoView = _assetsProvider.InstantiateAt<UFOView>(AssetPath.UFO,  position, rotation);
        UFOPresenter presenter = new UFOPresenter(new UFOModel(scoreReward), ufoView);

        presenter.Init(new UfoDamageReceiver(presenter), new UFODestroyer(presenter, _scoreCountService));
        
        _ufOsHolder.Add(presenter);
        
        return presenter;
    }

    public Bullet CreateBullet(Vector3 position, Quaternion rotation)
    {
        Bullet bullet = _assetsProvider.InstantiateAt<Bullet>(AssetPath.Bullet, position, rotation);
        
        _bulletsHolder.Add(bullet);
        
        return bullet;
    }

    public LaserBeam CreateLaserBeam(Vector2 position, Quaternion rotation) => 
        _assetsProvider.InstantiateAt<LaserBeam>(AssetPath.LaserBeam, position, rotation);

    public LoseWindow CreateLoseWindow(Transform parent) => 
        _assetsProvider.Instantiate<LoseWindow>(AssetPath.LoseWindow, parent);

    public PlayerStatsWindow CreatePlayerStatsWindow(Transform parent) => 
        _assetsProvider.Instantiate<PlayerStatsWindow>(AssetPath.PlayerStatsWindow, parent);

    public HUD CreateHUD() => 
        _assetsProvider.Instantiate<HUD>(AssetPath.HUD);
}