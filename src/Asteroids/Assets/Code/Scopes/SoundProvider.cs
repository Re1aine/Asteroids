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

    public AudioClip GetClipByType(SFXType type)
    {
        foreach (var vo in _config.SFXs)
        {
            if (vo.type != type)
                continue;

            var clipsLength = vo.clips.Length;
            var randomIndex = Random.Range(0, clipsLength);
            return vo.clips[randomIndex];
        }
        
        throw new Exception($"Doesn't exist settings with type : {type}");
    }
}

public interface ISoundProvider
{
    AudioClip GetClipByType(SFXType type);
}