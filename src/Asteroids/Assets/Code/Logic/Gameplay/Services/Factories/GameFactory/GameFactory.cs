using Code.Infrastructure.Common.AssetsManagement;
using Code.Infrastructure.Common.AssetsManagement.AssetsProvider;
using Code.Logic.Gameplay.Analytics.AnalyticsStore;
using Code.Logic.Gameplay.Entities.Enemy.Asteroid;
using Code.Logic.Gameplay.Entities.Enemy.UFO;
using Code.Logic.Gameplay.Entities.Player;
using Code.Logic.Gameplay.Projectiles.Bullet;
using Code.Logic.Gameplay.Projectiles.LaserBeam;
using Code.Logic.Gameplay.Services.Holders.AsteroidsHolder;
using Code.Logic.Gameplay.Services.Holders.BulletsHolder;
using Code.Logic.Gameplay.Services.Holders.RepositoriesHolder;
using Code.Logic.Gameplay.Services.Holders.UFOsHolder;
using Code.Logic.Gameplay.Services.Observers.Asteroid;
using Code.Logic.Gameplay.Services.Observers.UFO;
using Code.Logic.Gameplay.Services.Providers.HUDProvider;
using Code.Logic.Gameplay.Services.Providers.PlayerProvider;
using Code.Logic.Gameplay.Services.ScoreCounter;
using Code.UI.HUD;
using Code.UI.LoseWindow;
using Code.UI.PlayerStatsWindow;
using UnityEngine;
using VContainer;

namespace Code.Logic.Gameplay.Services.Factories.GameFactory
{
    public class GameFactory : IGameFactory
    {
        private readonly IScoreCountService _scoreCountService;
        private readonly IUFOsHolder _ufOsHolder;
        private readonly IAsteroidsHolder _asteroidsHolder;
        private readonly IBulletsHolder _bulletsHolder;
        private readonly IAssetsProvider _assetsProvider;
        private readonly IRepositoriesHolder _repositoriesHolder;
        private readonly IObjectResolver _resolver;
        private readonly IAnalyticsStore _analyticsStore;
        private readonly IVFXHolder _vfxHolder;
        
        public GameFactory(IScoreCountService scoreCountService,
            IUFOsHolder ufOsHolder,
            IAsteroidsHolder asteroidsHolder,
            IBulletsHolder bulletsHolder,
            IAssetsProvider assetsProvider,
            IRepositoriesHolder repositoriesHolder,
            IObjectResolver resolver,
            IAnalyticsStore analyticsStore,
            IVFXHolder vfxHolder)
        {
            _scoreCountService = scoreCountService;
            _ufOsHolder = ufOsHolder;
            _asteroidsHolder = asteroidsHolder;
            _bulletsHolder = bulletsHolder;
            _assetsProvider = assetsProvider;
            _repositoriesHolder = repositoriesHolder;
            _resolver = resolver;
            _analyticsStore = analyticsStore;
            _vfxHolder = vfxHolder;
        }

        public PlayerPresenter CreatePlayer(Vector3 position, Quaternion rotation)
        {
            PlayerView playerView = _assetsProvider.Instantiate<PlayerView>(AssetPath.Player);
            PlayerPresenter presenter = new PlayerPresenter(new PlayerModel(1), playerView);

            presenter.Init(
                new PlayerDamageReceiver(presenter),
                new PlayerDestroyer(presenter));
        
            return presenter;
        }

        public AsteroidPresenter CreateAsteroid(Vector3 position, Quaternion rotation, AsteroidType asteroidType, int scoreReward)
        {
            AsteroidView asteroidView = _assetsProvider.InstantiateAt<AsteroidView>(AssetPath.GetPathForAsteroid(asteroidType), position, rotation);
        
            AsteroidPresenter presenter = new AsteroidPresenter(new AsteroidModel(asteroidType, scoreReward), asteroidView);

            presenter.Init(
                new AsteroidDamageReceiver(presenter),
                new AsteroidDestroyer(presenter, this, _scoreCountService),
                new AsteroidDeathObserver(presenter, _analyticsStore),
                this);

            _asteroidsHolder.Add(presenter);
        
            return presenter;
        }

        public UFOPresenter CreateUfo(Vector3 position, Quaternion rotation, int scoreReward)
        {
            UFOView ufoView = _assetsProvider.InstantiateAt<UFOView>(AssetPath.UFO,  position, rotation);
            UFOPresenter presenter = new UFOPresenter(new UFOModel(scoreReward), ufoView);

            presenter.Init(
                new UfoDamageReceiver(presenter),
                new UFODestroyer(presenter, _scoreCountService),
                new UFODeathObserver(presenter, _analyticsStore),
                this);
        
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

        public LoseWindowPresenter CreateLoseWindow()
        {
            LoseWindowView view =  _assetsProvider.Instantiate<LoseWindowView>(AssetPath.LoseWindow, _resolver.Resolve<IHUDProvider>().HUD.View.transform);

            LoseWindowModel model = new LoseWindowModel(
                _scoreCountService,
                _repositoriesHolder);
            
             return new LoseWindowPresenter(model, view);
        }

        public PlayerStatsWindowPresenter CreatePlayerStatsWindow()
        {
            PlayerStatsWindowView view = _assetsProvider.Instantiate<PlayerStatsWindowView>(AssetPath.PlayerStatsWindow, _resolver.Resolve<IHUDProvider>().HUD.View.transform);

            PlayerStatsWindowModel model = new PlayerStatsWindowModel(
                _resolver.Resolve<IPlayerProvider>());
            
             return new PlayerStatsWindowPresenter(model, view);
        }

        public HUDPresenter CreateHUD()
        {
            HUDView hudView =  _assetsProvider.Instantiate<HUDView>(AssetPath.HUD);
            
            HUDModel model = new HUDModel();
            
            HUDService  hudService = new HUDService(this, model);
            
            return new HUDPresenter(model, hudView, hudService);
        }

        public VFX CreateVFX(VFXType type, Vector3 position, Quaternion rotation)
        {
            VFX vfx = _assetsProvider.InstantiateAt<VFX>(AssetPath.GetPathForVFX(type), position, rotation);
            
            _vfxHolder.Add(vfx);

            return vfx;
        }
    }
}

public enum VFXType
{
    None = 0,
    AsteroidDestroyVFX = 1,
    UFODestroyVFX = 2,
}