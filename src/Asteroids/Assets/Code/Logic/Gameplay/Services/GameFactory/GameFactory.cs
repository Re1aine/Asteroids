using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameFactory : IGameFactory
{
    private readonly PlayerView _playerPrefab = Resources.Load<PlayerView>("Player");
    private readonly UFOView _ufoPrefab = Resources.Load<UFOView>("UFO");
    private readonly Bullet _bulletPrefab = Resources.Load<Bullet>("Bullet");
    private readonly LaserBeam _laserBeamPrefab = Resources.Load<LaserBeam>("LaserBeam");
    private readonly HUD _hudPrefab = Resources.Load<HUD>("UI/HUD");
    private readonly LoseWindow _loseWindowPrefab = Resources.Load<LoseWindow>("UI/LoseWindow");
    private readonly PlayerStatsWindow _playerStatsWindowPrefab = Resources.Load<PlayerStatsWindow>("UI/PlayerStatsWindow");
    private readonly Dictionary<AsteroidType, AsteroidView> _asteroids = Resources.LoadAll<AsteroidView>("Asteroids")
        .ToDictionary(x => x.AsteroidType, x => x);

    private readonly IObjectResolver _resolver;
    private readonly IScoreCountService _scoreCountService;
    private readonly IUFOsHolder _ufOsHolder;
    private readonly IAsteroidsHolder _asteroidsHolder;
    private readonly IBulletsHolder _bulletsHolder;

    public GameFactory(IObjectResolver resolver, IScoreCountService scoreCountService, IUFOsHolder ufOsHolder, IAsteroidsHolder asteroidsHolder, IBulletsHolder bulletsHolder)
    {
        _resolver = resolver;
        _scoreCountService = scoreCountService;
        _ufOsHolder = ufOsHolder;
        _asteroidsHolder = asteroidsHolder;
        _bulletsHolder = bulletsHolder;
    }

    public PlayerPresenter CreatePlayer(Vector3 position, Quaternion rotation)
    {
        PlayerView playerView = _resolver.Instantiate(_playerPrefab, position, rotation);
        PlayerPresenter presenter = new PlayerPresenter(new PlayerModel(1), playerView);

        presenter.Init(new PlayerDamageReceiver(presenter), new PlayerDestroyer(presenter));
        playerView.Init(presenter);
        
        return presenter;
    }

    public AsteroidPresenter CreateAsteroid(Vector3 position, Quaternion rotation, AsteroidType asteroidType, int scoreReward)
    {
        AsteroidView prefab = _asteroids[asteroidType];

        AsteroidView asteroidView = _resolver.Instantiate(prefab, position, rotation);
        AsteroidPresenter presenter = new AsteroidPresenter(new AsteroidModel(asteroidType, scoreReward), asteroidView);

        presenter.Init(new AsteroidDamageReceiver(presenter), new AsteroidDestroyer(presenter, this, _scoreCountService));
        asteroidView.Init(presenter);

        _asteroidsHolder.Add(presenter);
        
        presenter.Destroyed += () => _asteroidsHolder.Remove(presenter);
        
        return presenter;
    }

    public UFOPresenter CreateUfo(Vector3 position, Quaternion rotation, int scoreReward)
    {
        UFOView ufoView = _resolver.Instantiate(_ufoPrefab, position, rotation);
        UFOPresenter presenter = new UFOPresenter(new UFOModel(scoreReward), ufoView);

        presenter.Init(new UfoDamageReceiver(presenter), new UFODestroyer(presenter, _scoreCountService));
        ufoView.Init(presenter);
        
        _ufOsHolder.Add(presenter);

        presenter.Destroyed += () => _ufOsHolder.Remove(presenter);
        
        return presenter;
    }

    public Bullet CreateBullet(Vector3 position, Quaternion rotation)
    {
        Bullet bullet =  _resolver.Instantiate(_bulletPrefab, position, rotation);
        
        _bulletsHolder.Add(bullet);
        bullet.Destroyed += () => _bulletsHolder.Remove(bullet);
        
        return bullet;
    }

    public LaserBeam CreateLaserBeam(Vector2 position, Quaternion rotation) => 
        _resolver.Instantiate(_laserBeamPrefab, position, rotation);

    public LoseWindow CreateLoseWindow(Transform parent) => 
        _resolver.Instantiate(_loseWindowPrefab, parent);

    public PlayerStatsWindow CreatePlayerStatsWindow(Transform parent) => 
        _resolver.Instantiate(_playerStatsWindowPrefab, parent);

    public HUD CreateHUD() => 
        _resolver.Instantiate(_hudPrefab);
}