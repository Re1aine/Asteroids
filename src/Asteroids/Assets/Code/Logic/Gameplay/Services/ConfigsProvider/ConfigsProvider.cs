using System.Collections.Generic;
using Code.Infrastructure.Common.AssetsManagement;
using Code.Infrastructure.Common.AssetsManagement.AssetLoader;
using Code.Logic.Gameplay.Audio;
using Cysharp.Threading.Tasks;

namespace Code.Logic.Gameplay.Services.ConfigsProvider
{
    public class ConfigsProvider : IConfigsProvider
    {
        private readonly IAddressablesAssetsLoader _assetsLoader;
    
        private AudioConfig _audioConfig;
        private VFXConfig _vfxConfig;
    
        public ConfigsProvider(IAddressablesAssetsLoader assetsLoader)
        {
            _assetsLoader = assetsLoader;
        }

        public async UniTask Initialize()
        {
            var tasks = new List<UniTask>
            {
                LoadVFXConfig(),
                LoadAudioConfig(),
            };

            await UniTask.WhenAll(tasks);
        }

        public AudioConfig GetAudioConfig() => 
            _audioConfig;

        public VFXConfig GetVFXConfig() => 
            _vfxConfig;

        private async UniTask LoadAudioConfig() => 
            _audioConfig = await _assetsLoader.LoadAsset<AudioConfig>(AssetsAddress.AudioConfig);

        private async UniTask LoadVFXConfig() => 
            _vfxConfig = await _assetsLoader.LoadAsset<VFXConfig>(AssetsAddress.VFXConfig);
    }
}