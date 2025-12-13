using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundProvider : ISoundProvider
{
    private readonly AudioConfig _config;

    public SoundProvider(AudioConfig config)
    {
        _config = config;
    }

    public AudioClip GetClipByType(SoundType type)
    {
        foreach (var sound in _config.Sounds)
        {
            if (sound.type != type)
                continue;

            var clipsLength = sound.clips.Length;
            var randomIndex = Random.Range(0, clipsLength);
            return sound.clips[randomIndex];
        }
        
        throw new Exception($"Doesn't exist settings with type : {type}");
    }
}

public interface ISoundProvider
{
    AudioClip GetClipByType(SoundType type);
}
