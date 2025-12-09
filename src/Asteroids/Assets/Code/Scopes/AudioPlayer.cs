using System.Linq;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] AudioChannel[] _audioChannels;
    
    public void Play(SFXType type, AudioClip clip)
    {
        var channel = GetChannel(type);
        channel.Play(type, clip);
    }

    public void StopAllShortSounds()
    { 
        _audioChannels
            .Where(x => x.AudioChannelType == AudioChannelType.ShortSounds)
            .ToList()
            .ForEach(x => x.Stop());
    }

    public void StopShortSound(SFXType type)
    {
        _audioChannels.Where(x => x.AudioChannelType == AudioChannelType.ShortSounds && x.SfxType == type)
            .ToList()
            .ForEach(x => x.Stop());
    }

    public void StopMusic()
    {
        var channel = GetChannel(SFXType.Music);
        channel.Stop();
    }
    
    private AudioChannel GetChannel(SFXType type)
    {
        if (type == SFXType.BulletShoot)
            return _audioChannels[0];
        else if(type == SFXType.LaserShoot)
            return _audioChannels[1];
        else
            return _audioChannels[2];
    }
}