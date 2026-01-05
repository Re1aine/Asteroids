using Code.Logic.Gameplay.Audio;
using Code.Logic.Gameplay.Services.ConfigsProvider.Configs.GameAssets;
using Code.Logic.Gameplay.Services.ConfigsProvider.Configs.GameBalance;
using Code.Logic.Gameplay.Services.ConfigsProvider.GameAssetsConfigProvider;
using Code.Logic.Gameplay.Services.ConfigsProvider.GameBalanceConfigsProvider;
using Cysharp.Threading.Tasks;

namespace Code.Logic.Gameplay.Services.ConfigsProvider
{
    public class GameConfigsProviderFacade : IGameConfigsProvider
    {
        private readonly IGameAssetsConfigsProvider _assetsConfigsProvider;
        private readonly IGameBalanceConfigsProvider _balanceConfigsProvider;
    
        public AudioConfig AudioConfig => _assetsConfigsProvider.AudioConfig;
        public VFXConfig VFXConfig => _assetsConfigsProvider.VFXConfig;
        public PlayerConfig PlayerConfig => _balanceConfigsProvider.PlayerConfig;
        public AsteroidConfig AsteroidConfig => _balanceConfigsProvider.AsteroidConfig;
        public GunConfig GunConfig => _balanceConfigsProvider.GunConfig;
        public UfoConfig UfoConfig => _balanceConfigsProvider.UfoConfig;
        public UfoSpawnerConfig UfoSpawnerConfig => _balanceConfigsProvider.UfoSpawnerConfig;
        public AsteroidSpawnerConfig AsteroidSpawnerConfig => _balanceConfigsProvider.AsteroidSpawnerConfig;
    
        public GameConfigsProviderFacade(IGameAssetsConfigsProvider assetsConfigsProvider, IGameBalanceConfigsProvider balanceConfigsProvider)
        {
            _assetsConfigsProvider = assetsConfigsProvider;
            _balanceConfigsProvider = balanceConfigsProvider;
        }

        public async UniTask Initialize()
        {
            await _assetsConfigsProvider.Initialize();
            await _balanceConfigsProvider.Initialize();
        }
    }
}