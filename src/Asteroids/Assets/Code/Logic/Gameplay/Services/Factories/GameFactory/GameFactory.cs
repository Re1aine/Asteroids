using System;
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
using Code.Logic.Gameplay.Services.AdService;
using Code.Logic.Gameplay.Services.ConfigsProvider;
using Code.Logic.Gameplay.Services.Holders.AsteroidsHolder;
using Code.Logic.Gameplay.Services.Holders.BulletsHolder;
using Code.Logic.Gameplay.Services.Holders.RepositoriesHolder;
using Code.Logic.Gameplay.Services.Holders.UFOsHolder;
using Code.Logic.Gameplay.Services.Holders.VFXHolder;
using Code.Logic.Gameplay.Services.Observers.Asteroid;
using Code.Logic.Gameplay.Services.Observers.UFO;
using Code.Logic.Gameplay.Services.Providers.HUDProvider;
using Code.Logic.Gameplay.Services.Providers.PlayerProvider;
using Code.Logic.Gameplay.Services.ScoreCounter;
using Code.UI.HUD;
using Code.UI.LoseWindow;
using Code.UI.PlayerStatsWindow;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using Object = UnityEngine.Object;

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
        private readonly IGameConfigsProvider _gameConfigsProvider;

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
            IGameConfigsProvider gameConfigsProvider)
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
            _gameConfigsProvider = gameConfigsProvider;
        }

        public async void WarmUp()
        {
            var (gameplayKeys, uiKeys) = await UniTask.WhenAll(
                _assetsLoader.GetAssetsListByLabel<GameObject>(AssetsAddress.Gameplay),
                _assetsLoader.GetAssetsListByLabel<GameObject>(AssetsAddress.UI)
            );
            
            await UniTask.WhenAll(
                _assetsLoader.LoadAll<GameObject>(gameplayKeys),
                _assetsLoader.LoadAll<GameObject>(uiKeys)
            );
        }
        
        public async UniTask<PlayerPresenter> CreatePlayer(Vector3 position, Quaternion rotation)
        {
            PlayerView playerView = await _addressablesAssetsProvider.Instantiate<PlayerView>(AssetsAddress.Player);
            
            PlayerPresenter presenter = new PlayerPresenter(
                new PlayerModel(_gameConfigsProvider.PlayerConfig, _gameConfigsProvider.GunConfig),
                playerView);

            presenter.Init(
                new PlayerDamageReceiver(presenter),
                new PlayerDestroyer(presenter));
        
            return presenter;
        }

        public async UniTask<AsteroidPresenter> CreateAsteroid(Vector3 position, Quaternion rotation, AsteroidType type, int scoreReward)
        {
            AsteroidView view = await _addressablesAssetsProvider.InstantiateAt<AsteroidView>(AssetsAddress.GetAddressForAsteroid(type), position, rotation);
            
            AsteroidPresenter presenter = new AsteroidPresenter(
                new AsteroidModel(type, _gameConfigsProvider.AsteroidConfig),
                view);

            presenter.Init(
                new AsteroidDamageReceiver(presenter),
                new AsteroidDestroyer(presenter, this, _scoreCountService),
                new AsteroidDeathObserver(presenter, _analyticsStore),
                this);

            _asteroidsHolder.Add(presenter);
        
            return presenter;
        }

        public async UniTask<UFOPresenter> CreateUfo(Vector3 position, Quaternion rotation, int scoreReward)
        {
            UFOView view = await _addressablesAssetsProvider.InstantiateAt<UFOView>(AssetsAddress.UFO, position, rotation);
            
            UFOPresenter presenter = new UFOPresenter(
                new UFOModel(_gameConfigsProvider.UfoConfig),
                view);

            presenter.Init(
                new UfoDamageReceiver(presenter),
                new UFODestroyer(presenter, _scoreCountService),
                new UFODeathObserver(presenter, _analyticsStore),
                this);
        
            _ufOsHolder.Add(presenter);
        
            return presenter;
        }

        public async UniTask<Bullet> CreateBullet(Vector3 position, Quaternion rotation)
        {
            Bullet bullet = await _addressablesAssetsProvider.InstantiateAt<Bullet>(AssetsAddress.Bullet, position, rotation);
            
            _bulletsHolder.Add(bullet);
        
            return bullet;
        }

        public async UniTask<LaserBeam> CreateLaserBeam(Vector2 position, Quaternion rotation) => 
            await _addressablesAssetsProvider.InstantiateAt<LaserBeam>(AssetsAddress.LaserBeam,position, rotation);

        public async UniTask<LoseWindowPresenter> CreateLoseWindow()
        {
            LoseWindowView view =  await _addressablesAssetsProvider.Instantiate<LoseWindowView>(AssetsAddress.LoseWindow, _resolver.Resolve<IHUDProvider>().HUD.View.transform);
            
            LoseWindowModel model = new LoseWindowModel(
                _scoreCountService,
                _repositoriesHolder);
            
             return new LoseWindowPresenter(model, view);
        }

        public async UniTask<PlayerStatsWindowPresenter> CreatePlayerStatsWindow()
        {
            PlayerStatsWindowView view = await _addressablesAssetsProvider.Instantiate<PlayerStatsWindowView>(AssetsAddress.PlayerStatsWindow, _resolver.Resolve<IHUDProvider>().HUD.View.transform);
            
            PlayerStatsWindowModel model = new PlayerStatsWindowModel(
                _resolver.Resolve<IPlayerProvider>());
            
             return new PlayerStatsWindowPresenter(model, view);
        }

        public async UniTask<HUDPresenter> CreateHUD()
        {
            HUDView view =  await _addressablesAssetsProvider.Instantiate<HUDView>(AssetsAddress.HUD);
            
            HUDModel model = new HUDModel(new HUDService(this));
            
            return new HUDPresenter(model, view);
        }

        public async UniTask<ReviveWindowPresenter> CreateRevivedWindow()
        {
            ReviveWindowView view = await _addressablesAssetsProvider.Instantiate<ReviveWindowView>(AssetsAddress.ReviveWindow,
                _resolver.Resolve<IHUDProvider>().HUD.View.transform);

            ReviveWindowModel model = new ReviveWindowModel(_resolver.Resolve<IAdsService>());
            
            return new ReviveWindowPresenter(model, view);
        }

        public VFX CreateVFX(VFXType type, Vector3 position, Quaternion rotation)
        {
            VFX prefab = _gameConfigsProvider
                .VFXConfig
                .GetVFXByType(type);

            VFX vfx = Object.Instantiate(prefab, position, rotation);
            
            _vfxHolder.Add(vfx);

            return vfx;
        }
    }
}