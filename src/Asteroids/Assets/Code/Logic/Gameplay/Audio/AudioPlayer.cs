using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioChannel[] _audioChannels;
    
    private Dictionary<SoundType, SoundCategory> _channelsMap;

    private void Awake()
    {
        _channelsMap = new()
        {
            [SoundType.BulletShoot] = SoundCategory.ShortSounds,
            [SoundType.LaserShoot] = SoundCategory.ShortSounds,
            [SoundType.Music] = SoundCategory.Music,
        };
    }

    public void PlaySound(SoundType type, AudioClip clip)
    {
        var channel = GetChannel(type);
        channel.Play(clip);
    }
    
    public void StopSoundCategory(SoundCategory category)
    {
        _audioChannels
            .Where(x  => x.SoundCategory == category)
            .ToList()
            .ForEach(x => x.Stop());
    }

    public void PauseSoundCategory(SoundCategory category)
    {
        _audioChannels
            .Where(x  => x.SoundCategory == category)
            .ToList()
            .ForEach(x => x.Pause());
    }

    public void UnPauseSoundCategory(SoundCategory category)
    {
        _audioChannels
            .Where(x => x.SoundCategory == category)
            .ToList()
            .ForEach(x => x.UnPause());
    }
    
    public void StopSound(SoundType type)
    {
        var channel = GetChannel(type);    
        channel.Stop();
    }
    
    private AudioChannel GetChannel(SoundType type)
    {
        var  channel = _channelsMap[type];
        return _audioChannels
            .ToList()
            .Find(x => x.SoundCategory == channel && x.SoundType == type);
    }

    public void StopAllSounds()
    {
        _audioChannels
            .ToList()
            .ForEach(x => x.Stop());
    }

    public void Reset()
    {
        foreach (var channel in _audioChannels) 
            channel.Reset();
    }
}