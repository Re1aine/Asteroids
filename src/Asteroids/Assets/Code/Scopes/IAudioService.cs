using System.Threading.Tasks;

public interface IAudioService
{
    Task Initialize();
    void PlaySound(SoundType type);
    void StopSound(SoundType type);
    void StopSoundCategory(SoundCategory category);
    void StopAllSounds();
}