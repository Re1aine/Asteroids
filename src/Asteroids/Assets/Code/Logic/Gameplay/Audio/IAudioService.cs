
public interface IAudioService
{
    void Initialize();
    void PlaySound(SoundType type);
    void StopSound(SoundType type);
    void StopSoundCategory(SoundCategory category);
    void PauseSoundCategory(SoundCategory category);
    void StopAllSounds();
    void Reset();
}