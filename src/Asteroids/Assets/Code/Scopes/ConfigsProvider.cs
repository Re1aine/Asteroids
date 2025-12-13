using System.Threading.Tasks;
using Code.Infrastructure.Common.AssetsManagement;
using Code.Infrastructure.Common.AssetsManagement.AssetLoader;

public class ConfigsProvider : IConfigsProvider
{
    private readonly IAddressablesAssetsLoader _assetsLoader;
    
    private AudioConfig _audioConfig;
    
    public ConfigsProvider(IAddressablesAssetsLoader assetsLoader)
    {
        _assetsLoader = assetsLoader;
    }

    public async Task Initialize()
    {
        await LoadAudioConfig();
    }

    public AudioConfig GetAudioConfig() => 
        _audioConfig;

    private async Task LoadAudioConfig() => 
        _audioConfig = await _assetsLoader.LoadAsset<AudioConfig>(AssetsAddress.AudioConfig);
}