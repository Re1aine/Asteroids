using Code.Logic.Gameplay.Services.Factories.GameFactory;
using UnityEngine;

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

    public void Initialize() => 
        _audioPlayer = _gameFactory.CreateAudioPlayer();
    
    public void PlayShortSound(SFXType type)
    {
        var clip = _soundProvider.GetClipByType(type);
        _audioPlayer.Play(type, clip);
    }

    public void PlayMusic()
    {
        var clip = _soundProvider.GetClipByType(SFXType.Music);
        _audioPlayer.Play(SFXType.Music, clip);
    }

    public void StopShortSound(SFXType type) => 
        _audioPlayer.StopShortSound(type);

    public void StopAllShortSounds() => 
        _audioPlayer.StopAllShortSounds();

    public void StopMusic() => 
        _audioPlayer.StopMusic();

    public void StopAllSounds()
    {
        StopAllShortSounds();
        StopMusic();
    }
}

public interface IAudioService
{
    void Initialize();
    void PlayShortSound(SFXType type);
    void StopShortSound(SFXType type);
    void StopAllShortSounds();
    void StopAllSounds();
    void PlayMusic();
    void StopMusic();
}