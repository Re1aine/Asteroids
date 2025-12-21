using System;
using UnityEngine;

namespace Code.Logic.Gameplay.Audio
{
    [Serializable]
    public struct SoundSettings
    {
        public SoundType type;
        public AudioClip[] clips; 
    }
}