using System.Threading.Tasks;
using Code.Infrastructure.Common.AssetsManagement;
using Code.Infrastructure.Common.AssetsManagement.AssetLoader;

public class ConfigsProvider : IConfigsProvider
{
    private readonly IAddressablesAssetsLoader _assetsLoader;
    
    private AudioConfig _audioConfig;
    private VFXConfig _vfxConfig;
    
    public ConfigsProvider(IAddressablesAssetsLoader assetsLoader)
    {
        _assetsLoader = assetsLoader;
    }

    public async Task Initialize()
    {
        await LoadAudioConfig();
        await LoadVFXConfig();
    }

    public AudioConfig GetAudioConfig() => 
        _audioConfig;

    public VFXConfig GetVFXConfig() => 
        _vfxConfig;
    
    private async Task LoadAudioConfig() => 
        _audioConfig = await _assetsLoader.LoadAsset<AudioConfig>(AssetsAddress.AudioConfig);

    private async Task LoadVFXConfig() => 
        _vfxConfig = await _assetsLoader.LoadAsset<VFXConfig>(AssetsAddress.VFXConfig);
}