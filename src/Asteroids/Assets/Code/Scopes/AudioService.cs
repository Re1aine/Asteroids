using Code.Logic.Gameplay.Services.Factories.GameFactory;

public class AudioService : IAudioService
{
    private readonly ISoundProvider _soundProvider;
    private readonly IGameFactory _gameFactory;
    
    private AudioPlayer _audioPlayer;
    
    public AudioService(ISoundProvider soundProvider, IGameFactory gameFactory)
    {
        _soundProvider = soundProvider;
        _gameFactory = gameFactory;
    }

    public async void Initialize() => 
        _audioPlayer = await _gameFactory.CreateAudioPlayer();

    public void PlaySound(SoundType type)
    {
        var clip = _soundProvider.GetClipByType(type);
        _audioPlayer.PlaySound(type, clip);
    }
    
    public void StopSound(SoundType type) => 
        _audioPlayer.StopSound(type);

    public void StopSoundCategory(SoundCategory category) => 
        _audioPlayer.StopSoundCategory(category);
    
    public void StopAllSounds() => 
        _audioPlayer.StopAllSounds();
}