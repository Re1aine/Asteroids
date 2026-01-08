using Code.Infrastructure.Common.AssetsManagement;
using Code.Infrastructure.Common.AssetsManagement.AssetLoader;
using Code.Infrastructure.Common.AssetsManagement.AssetProvider;
using Code.Logic.Gameplay.Analytics.AnalyticsStore;
using Code.Logic.Gameplay.Entities.Enemy.Asteroid;
using Code.Logic.Gameplay.Entities.Enemy.UFO;
using Code.Logic.Gameplay.Entities.Player;
using Code.Logic.Gameplay.Projectiles.Bullet;
using Code.Logic.Gameplay.Projectiles.LaserBeam;
using Code.Logic.Gameplay.Services.Configs;
using Code.Logic.Gameplay.Services.Configs.Configs.Assets;
using Code.Logic.Gameplay.Services.Holders.AsteroidsHolder;
using Code.Logic.Gameplay.Services.Holders.BulletsHolder;
using Code.Logic.Gameplay.Services.Holders.UFOsHolder;
using Code.Logic.Gameplay.Services.Holders.VFXHolder;
using Code.Logic.Gameplay.Services.Observers.Asteroid;
using Code.Logic.Gameplay.Services.Observers.UFO;
using Code.Logic.Gameplay.Services.ScoreCounter;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Logic.Gameplay.Services.Factories.GameFactory
{
    public class GameFactory : IGameFactory
    {
        private readonly IScoreCountService _scoreCountService;
        private readonly IUFOsHolder _ufOsHolder;
        private readonly IAsteroidsHolder _asteroidsHolder;
        private readonly IBulletsHolder _bulletsHolder;
        private readonly IAnalyticsStore _analyticsStore;
        private readonly IAddressablesAssetsProvider _addressablesAssetsProvider;
        private readonly IAddressablesAssetsLoader _addressablesAssetsLoader;
        private readonly IVFXHolder _vfxHolder;
        private readonly IConfigsProvider _configsProvider;

        public GameFactory(IScoreCountService scoreCountService,
            IUFOsHolder ufOsHolder,
            IAsteroidsHolder asteroidsHolder,
            IBulletsHolder bulletsHolder,
            IAnalyticsStore analyticsStore,
            IAddressablesAssetsProvider addressablesAssetsProvider,
            IAddressablesAssetsLoader addressablesAssetsLoader,
            IVFXHolder vfxHolder,
            IConfigsProvider configsProvider)
        {
            _scoreCountService = scoreCountService;
            _ufOsHolder = ufOsHolder;
            _asteroidsHolder = asteroidsHolder;
            _bulletsHolder = bulletsHolder;
            _analyticsStore = analyticsStore;
            _addressablesAssetsProvider = addressablesAssetsProvider;
            _addressablesAssetsLoader = addressablesAssetsLoader;
            _vfxHolder = vfxHolder;
            _configsProvider = configsProvider;
        }

        public async UniTask WarmUp()
        {
            var (gameplayKeys, uiKeys) = await UniTask.WhenAll(
                _addressablesAssetsLoader.GetAssetsListByLabel<GameObject>(AssetsAddress.Gameplay),
                _addressablesAssetsLoader.GetAssetsListByLabel<GameObject>(AssetsAddress.UI)
            );
            
            await UniTask.WhenAll(
                _addressablesAssetsLoader.LoadAll<GameObject>(gameplayKeys),
                _addressablesAssetsLoader.LoadAll<GameObject>(uiKeys)
            );
        }
        
        public async UniTask<PlayerPresenter> CreatePlayer(Vector3 position, Quaternion rotation)
        {
            PlayerView playerView = await _addressablesAssetsProvider.Instantiate<PlayerView>(AssetsAddress.Player);
            
            PlayerPresenter presenter = new PlayerPresenter(
                new PlayerModel(_configsProvider.PlayerConfig, _configsProvider.GunConfig),
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
                new AsteroidModel(type, _configsProvider.AsteroidConfig),
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
                new UFOModel(_configsProvider.UfoConfig),
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
        public VFX CreateVFX(VFXType type, Vector3 position, Quaternion rotation)
        {
            VFX prefab = _configsProvider
                .VFXConfig
                .GetVFXByType(type);

            VFX vfx = Object.Instantiate(prefab, position, rotation);
            
            _vfxHolder.Add(vfx);

            return vfx;
        }
    }
}