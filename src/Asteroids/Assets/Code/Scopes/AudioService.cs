using System.Threading.Tasks;
using Code.Logic.Gameplay.Services.Factories.GameFactory;

public class AudioService : IAudioService
{
    private readonly IConfigsProvider _configsProvider;
    private readonly IGameFactory _gameFactory;
    
    private AudioPlayer _audioPlayer;
    private AudioConfig _audioConfig;

    public AudioService(IConfigsProvider configsProvider, IGameFactory gameFactory)
    {
        _configsProvider = configsProvider;
        _gameFactory = gameFactory;
    }

    public async Task Initialize()
    {
        _audioPlayer = await _gameFactory.CreateAudioPlayer();
        _audioConfig = _configsProvider.GetAudioConfig();
    }

    public void PlaySound(SoundType type)
    {
        var clip = _audioConfig.GetRandomClipByType(type);
        _audioPlayer.PlaySound(type, clip);
    }
    
    public void StopSound(SoundType type) => 
        _audioPlayer.StopSound(type);

    public void StopSoundCategory(SoundCategory category) => 
        _audioPlayer.StopSoundCategory(category);
    
    public void StopAllSounds() => 
        _audioPlayer.StopAllSounds();
}