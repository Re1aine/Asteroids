public class ConfigsProvider : IConfigsProvider
{
    private readonly AudioConfig _audioConfig;
    
    public ConfigsProvider(AudioConfig config)
    {
        _audioConfig = config;
    }

    public AudioConfig GetAudioConfig() => 
        _audioConfig;
}