using _Project.Code.Logic.Gameplay.Audio;
using _Project.Code.Logic.Gameplay.Services.Configs.AssetsConfigProvider;
using _Project.Code.Logic.Gameplay.Services.Configs.BalanceConfigsProvider;
using _Project.Code.Logic.Gameplay.Services.Configs.Configs.Assets;
using _Project.Code.Logic.Gameplay.Services.Configs.Configs.Balance;
using Cysharp.Threading.Tasks;

namespace _Project.Code.Logic.Gameplay.Services.Configs
{
    public class ConfigsProviderFacade : IConfigsProvider
    {
        private readonly IAssetsConfigsProvider _assetsConfigsProvider;
        private readonly IBalanceConfigsProvider _balanceConfigsProvider;
    
        public AudioConfig AudioConfig => _assetsConfigsProvider.AudioConfig;
        public VFXConfig VFXConfig => _assetsConfigsProvider.VFXConfig;
        public PlayerConfig PlayerConfig => _balanceConfigsProvider.PlayerConfig;
        public AsteroidConfig AsteroidConfig => _balanceConfigsProvider.AsteroidConfig;
        public GunConfig GunConfig => _balanceConfigsProvider.GunConfig;
        public UfoConfig UfoConfig => _balanceConfigsProvider.UfoConfig;
        public UfoSpawnerConfig UfoSpawnerConfig => _balanceConfigsProvider.UfoSpawnerConfig;
        public AsteroidSpawnerConfig AsteroidSpawnerConfig => _balanceConfigsProvider.AsteroidSpawnerConfig;
    
        public ConfigsProviderFacade(IAssetsConfigsProvider assetsConfigsProvider, IBalanceConfigsProvider balanceConfigsProvider)
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