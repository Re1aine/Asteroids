using System.Collections.Generic;
using Code.Infrastructure.Common.AssetsManagement;
using Code.Infrastructure.Common.AssetsManagement.AssetLoader;
using Code.Logic.Gameplay.Audio;
using Cysharp.Threading.Tasks;

namespace Code.Logic.Gameplay.Services.ConfigsProvider
{
    public class GameAssetsConfigsProvider : IGameAssetsConfigsProvider
    {
        private readonly IAddressablesAssetsLoader _assetsLoader;
        
        public AudioConfig AudioConfig { get; private set; }
        public VFXConfig VFXConfig { get; private set; }

        public GameAssetsConfigsProvider(IAddressablesAssetsLoader assetsLoader) => 
            _assetsLoader = assetsLoader;

        public async UniTask Initialize()
        {
            var tasks = new List<UniTask>
            {
                LoadVFXConfig(),
                LoadAudioConfig(),
            };

            await UniTask.WhenAll(tasks);
        }
        
        private async UniTask LoadAudioConfig() => 
            AudioConfig = await _assetsLoader.LoadAsset<AudioConfig>(AssetsAddress.AudioConfig);

        private async UniTask LoadVFXConfig() => 
            VFXConfig = await _assetsLoader.LoadAsset<VFXConfig>(AssetsAddress.VFXConfig);
    }
}