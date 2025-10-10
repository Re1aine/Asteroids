using Code.Logic.Gameplay.Entities.Enemy.Asteroid;
using Code.Logic.Gameplay.Entities.Enemy.UFO;
using Code.Logic.Gameplay.Entities.Player;
using Code.Logic.Gameplay.Projectiles.Bullet;
using Code.Logic.Gameplay.Projectiles.LaserBeam;
using Code.Logic.Gameplay.Services.Holders.AsteroidsHolder;
using Code.Logic.Gameplay.Services.Holders.BulletsHolder;
using Code.Logic.Gameplay.Services.Holders.UFOsHolder;
using Code.Logic.Gameplay.Services.ScoreCounter;
using Code.UI.HUD;
using Code.UI.LoseWindow;
using Code.UI.PlayerStatsWindow;
using UnityEngine;

namespace Code.Logic.Gameplay.Services.Factories.GameFactory
{
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

        public LoseWindowPresenter CreateLoseWindow(Transform parent)
        {
            LoseWindowView view =  _assetsProvider.Instantiate<LoseWindowView>(AssetPath.LoseWindow, parent);
            LoseWindowPresenter presenter = new LoseWindowPresenter(new LoseWindowModel(), view);
            
            return presenter;
        }

        public PlayerStatsWindowPresenter CreatePlayerStatsWindow(Transform parent)
        {
            PlayerStatsWindowView view = _assetsProvider.Instantiate<PlayerStatsWindowView>(AssetPath.PlayerStatsWindow, parent);
            PlayerStatsWindowPresenter presenter = new PlayerStatsWindowPresenter(new PlayerStatsWindowModel(), view);
            
            return presenter;
        }

        public HUDPresenter CreateHUD()
        {
            HUDView hudView =  _assetsProvider.Instantiate<HUDView>(AssetPath.HUD);
            
            HUDPresenter presenter = new HUDPresenter(new HUDModel(), hudView);
            
            presenter.Init(this, _scoreCountService);
            
            return presenter;
        }
    }
}