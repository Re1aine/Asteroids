using System.Threading.Tasks;
using Code.Infrastructure.Common.AssetsManagement;
using Code.Infrastructure.Common.AssetsManagement.AssetLoader;
using Code.Infrastructure.Common.AssetsManagement.AssetProvider;
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
        private readonly IRepositoriesHolder _repositoriesHolder;
        private readonly IObjectResolver _resolver;
        private readonly IAnalyticsStore _analyticsStore;
        private readonly IAddressablesAssetsProvider _addressablesAssetsProvider;
        private readonly IAddressablesAssetsLoader _assetsLoader;
        private readonly IVFXHolder _vfxHolder;
        private readonly IConfigsProvider _configsProvider;

        public GameFactory(IScoreCountService scoreCountService,
            IUFOsHolder ufOsHolder,
            IAsteroidsHolder asteroidsHolder,
            IBulletsHolder bulletsHolder,
            IRepositoriesHolder repositoriesHolder,
            IObjectResolver resolver,
            IAnalyticsStore analyticsStore,
            IAddressablesAssetsProvider addressablesAssetsProvider,
            IAddressablesAssetsLoader assetsLoader,
            IVFXHolder vfxHolder,
            IConfigsProvider configsProvider)
        {
            _scoreCountService = scoreCountService;
            _ufOsHolder = ufOsHolder;
            _asteroidsHolder = asteroidsHolder;
            _bulletsHolder = bulletsHolder;
            _repositoriesHolder = repositoriesHolder;
            _resolver = resolver;
            _analyticsStore = analyticsStore;
            _addressablesAssetsProvider = addressablesAssetsProvider;
            _assetsLoader = assetsLoader;
            _vfxHolder = vfxHolder;
            _configsProvider = configsProvider;
        }

        public async void WarmUp()
        {
            await _assetsLoader.LoadAsset<GameObject>(AssetsAddress.Bullet);
        }
        
        public async Task<PlayerPresenter> CreatePlayer(Vector3 position, Quaternion rotation)
        {
            PlayerView playerView = await _addressablesAssetsProvider.Instantiate<PlayerView>(AssetsAddress.Player);
            
            PlayerPresenter presenter = new PlayerPresenter(new PlayerModel(1),  playerView);

            presenter.Init(
                new PlayerDamageReceiver(presenter),
                new PlayerDestroyer(presenter));
        
            return presenter;
        }

        public async Task<AsteroidPresenter> CreateAsteroid(Vector3 position, Quaternion rotation, AsteroidType type, int scoreReward)
        {
            AsteroidView view = await _addressablesAssetsProvider.InstantiateAt<AsteroidView>(AssetsAddress.GetAddressForAsteroid(type), position, rotation);
            
            AsteroidPresenter presenter = new AsteroidPresenter(new AsteroidModel(type, scoreReward), view);

            presenter.Init(
                new AsteroidDamageReceiver(presenter),
                new AsteroidDestroyer(presenter, this, _scoreCountService),
                new AsteroidDeathObserver(presenter, _analyticsStore),
                this);

            _asteroidsHolder.Add(presenter);
        
            return presenter;
        }

        public async Task<UFOPresenter> CreateUfo(Vector3 position, Quaternion rotation, int scoreReward)
        {
            UFOView view = await _addressablesAssetsProvider.InstantiateAt<UFOView>(AssetsAddress.UFO, position, rotation);
            
            UFOPresenter presenter = new UFOPresenter(new UFOModel(scoreReward), view);

            presenter.Init(
                new UfoDamageReceiver(presenter),
                new UFODestroyer(presenter, _scoreCountService),
                new UFODeathObserver(presenter, _analyticsStore),
                this);
        
            _ufOsHolder.Add(presenter);
        
            return presenter;
        }

        public async Task<Bullet> CreateBullet(Vector3 position, Quaternion rotation)
        {
            Bullet bullet = await _addressablesAssetsProvider.InstantiateAt<Bullet>(AssetsAddress.Bullet, position, rotation);
            
            _bulletsHolder.Add(bullet);
        
            return bullet;
        }

        public async Task<LaserBeam> CreateLaserBeam(Vector2 position, Quaternion rotation) => 
            await _addressablesAssetsProvider.InstantiateAt<LaserBeam>(AssetsAddress.LaserBeam,position, rotation);

        public async Task<LoseWindowPresenter> CreateLoseWindow()
        {
            LoseWindowView view =  await _addressablesAssetsProvider.Instantiate<LoseWindowView>(AssetsAddress.LoseWindow, _resolver.Resolve<IHUDProvider>().HUD.View.transform);
            
            LoseWindowModel model = new LoseWindowModel(
                _scoreCountService,
                _repositoriesHolder);
            
             return new LoseWindowPresenter(model, view);
        }

        public async Task<PlayerStatsWindowPresenter> CreatePlayerStatsWindow()
        {
            PlayerStatsWindowView view = await _addressablesAssetsProvider.Instantiate<PlayerStatsWindowView>(AssetsAddress.PlayerStatsWindow, _resolver.Resolve<IHUDProvider>().HUD.View.transform);
            
            PlayerStatsWindowModel model = new PlayerStatsWindowModel(
                _resolver.Resolve<IPlayerProvider>());
            
             return new PlayerStatsWindowPresenter(model, view);
        }

        public async Task<HUDPresenter> CreateHUD()
        {
            HUDView view =  await _addressablesAssetsProvider.Instantiate<HUDView>(AssetsAddress.HUD);
            
            HUDModel model = new HUDModel(new HUDService(this));
            
            return new HUDPresenter(model, view);
        }

        public VFX CreateVFX(VFXType type, Vector3 position, Quaternion rotation)
        {
            VFX prefab = _configsProvider
                .GetVFXConfig()
                .GetVFXByType(type);

            VFX vfx = Object.Instantiate(prefab, position, rotation);
            
            _vfxHolder.Add(vfx);

            return vfx;
        }
    }
}