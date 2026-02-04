using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Code.Logic.Gameplay.Audio
{
    [CreateAssetMenu(fileName = "AudioConfig", menuName = "Configs/AudioConfig")]
    public class AudioConfig : ScriptableObject
    {
        public SoundSettings[] Sounds;
    
        public AudioClip GetRandomClipByType(SoundType type)
        {
            foreach (var sound in Sounds)
            {
                if (sound.type != type)
                    continue;

                var clipsLength = sound.clips.Length;
                var randomIndex = Random.Range(0, clipsLength);
                return sound.clips[randomIndex];
            }
        
            throw new Exception($"Doesn't exist SoundsSetting with SoundType : {type}");
        }
    }
}