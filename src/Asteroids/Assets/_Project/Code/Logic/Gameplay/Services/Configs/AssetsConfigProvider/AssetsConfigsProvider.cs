using System.Collections.Generic;
using _Project.Code.Infrastructure.Common.AssetsManagement;
using _Project.Code.Infrastructure.Common.AssetsManagement.AssetLoader;
using _Project.Code.Logic.Gameplay.Audio;
using _Project.Code.Logic.Gameplay.Services.Configs.Configs.Assets;
using Cysharp.Threading.Tasks;

namespace _Project.Code.Logic.Gameplay.Services.Configs.AssetsConfigProvider
{
    public class AssetsConfigsProvider : IAssetsConfigsProvider
    {
        private readonly IAddressablesAssetsLoader _assetsLoader;
        
        public AudioConfig AudioConfig { get; private set; }
        public VFXConfig VFXConfig { get; private set; }

        public AssetsConfigsProvider(IAddressablesAssetsLoader assetsLoader) => 
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