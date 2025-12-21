using Code.Logic.Gameplay.Services.ConfigsProvider;

namespace Code.Logic.Gameplay.Audio
{
    public class AudioService : IAudioService
    {
        private readonly IConfigsProvider _configsProvider;
    
        private AudioPlayer _audioPlayer;
        private AudioConfig _audioConfig;

        public AudioService(IConfigsProvider configsProvider, AudioPlayer audioPlayer)
        {
            _configsProvider = configsProvider;
            _audioPlayer = audioPlayer;
        }
    
        public void Initialize() => 
            _audioConfig = _configsProvider.GetAudioConfig();

        public void PlaySound(SoundType type)
        {
            var clip = _audioConfig.GetRandomClipByType(type);
            _audioPlayer.PlaySound(type, clip);
        }
    
        public void StopSound(SoundType type) => 
            _audioPlayer.StopSound(type);

        public void StopSoundCategory(SoundCategory category) => 
            _audioPlayer.StopSoundCategory(category);

        public void PauseSoundCategory(SoundCategory category) => 
            _audioPlayer.PauseSoundCategory(category);

        public void UnPauseSoundCategory(SoundCategory category) => 
            _audioPlayer.UnPauseSoundCategory(category);

        public void StopAllSounds() => 
            _audioPlayer.StopAllSounds();

        public void Reset()
        {
            StopAllSounds();
            _audioPlayer.Reset();
        }
    }
}