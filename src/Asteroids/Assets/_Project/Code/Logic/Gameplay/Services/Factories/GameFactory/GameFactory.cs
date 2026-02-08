using _Project.Code.Infrastructure.Common.AssetsManagement;
using _Project.Code.Infrastructure.Common.AssetsManagement.AssetProvider;
using _Project.Code.Logic.Gameplay.Analytics.AnalyticsStore;
using _Project.Code.Logic.Gameplay.Entities.Enemy.Asteroid;
using _Project.Code.Logic.Gameplay.Entities.Enemy.UFO;
using _Project.Code.Logic.Gameplay.Entities.Player;
using _Project.Code.Logic.Gameplay.Projectiles.Bullet;
using _Project.Code.Logic.Gameplay.Projectiles.LaserBeam;
using _Project.Code.Logic.Gameplay.Services.Configs.AssetsConfigProvider;
using _Project.Code.Logic.Gameplay.Services.Configs.BalanceConfigsProvider;
using _Project.Code.Logic.Gameplay.Services.Configs.Configs.Assets;
using _Project.Code.Logic.Gameplay.Services.Holders.AsteroidsHolder;
using _Project.Code.Logic.Gameplay.Services.Holders.BulletsHolder;
using _Project.Code.Logic.Gameplay.Services.Holders.UFOsHolder;
using _Project.Code.Logic.Gameplay.Services.Holders.VFXHolder;
using _Project.Code.Logic.Gameplay.Services.Observers.Asteroid;
using _Project.Code.Logic.Gameplay.Services.Observers.UFO;
using _Project.Code.Logic.Gameplay.Services.ScoreCounter;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Code.Logic.Gameplay.Services.Factories.GameFactory
{
    public class GameFactory : IGameFactory
    {
        private readonly IScoreCountService _scoreCountService;
        private readonly IUFOsHolder _ufOsHolder;
        private readonly IAsteroidsHolder _asteroidsHolder;
        private readonly IBulletsHolder _bulletsHolder;
        private readonly IAnalyticsStore _analyticsStore;
        private readonly IAddressablesAssetsProvider _addressablesAssetsProvider;
        private readonly IVFXHolder _vfxHolder;
        private readonly IBalanceConfigsProvider _balanceConfigsProvider;
        private readonly IAssetsConfigsProvider _assetsConfigsProvider;

        public GameFactory(IScoreCountService scoreCountService,
            IUFOsHolder ufOsHolder,
            IAsteroidsHolder asteroidsHolder,
            IBulletsHolder bulletsHolder,
            IAnalyticsStore analyticsStore,
            IAddressablesAssetsProvider addressablesAssetsProvider,
            IVFXHolder vfxHolder,
            IBalanceConfigsProvider balanceConfigsProvider,
            IAssetsConfigsProvider assetsConfigsProvider)
        {
            _scoreCountService = scoreCountService;
            _ufOsHolder = ufOsHolder;
            _asteroidsHolder = asteroidsHolder;
            _bulletsHolder = bulletsHolder;
            _analyticsStore = analyticsStore;
            _addressablesAssetsProvider = addressablesAssetsProvider;
            _vfxHolder = vfxHolder;
            _balanceConfigsProvider = balanceConfigsProvider;
            _assetsConfigsProvider = assetsConfigsProvider;
        }
        
        public async UniTask<PlayerPresenter> CreatePlayer(Vector3 position, Quaternion rotation)
        {
            PlayerView playerView = await _addressablesAssetsProvider.Instantiate<PlayerView>(AssetsAddress.Player);
            
            PlayerPresenter presenter = new PlayerPresenter(
                new PlayerModel(_balanceConfigsProvider.PlayerConfig, _balanceConfigsProvider.GunConfig),
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
                new AsteroidModel(type, _balanceConfigsProvider.AsteroidConfig),
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
                new UFOModel(_balanceConfigsProvider.UfoConfig),
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
            VFX prefab = _assetsConfigsProvider
                .VFXConfig
                .GetVFXByType(type);

            VFX vfx = Object.Instantiate(prefab, position, rotation);
            
            _vfxHolder.Add(vfx);

            return vfx;
        }
    }
}