using _Project.Code.Logic.Gameplay.Services.Configs;
using _Project.Code.Logic.Gameplay.Services.Configs.AssetsConfigProvider;

namespace _Project.Code.Logic.Gameplay.Audio
{
    public class AudioService : IAudioService
    {
        private readonly AudioPlayer _audioPlayer;
        private AudioConfig _audioConfig;
        
        private readonly IAssetsConfigsProvider _assetConfigProvider;
        
        public AudioService(IAssetsConfigsProvider assetsConfigsProvider, AudioPlayer audioPlayer)
        {
            _assetConfigProvider =  assetsConfigsProvider;
            _audioPlayer = audioPlayer;
        }
    
        public void Initialize() => 
            _audioConfig = _assetConfigProvider.AudioConfig;

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