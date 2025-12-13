using System;
using UnityEngine;

[Serializable]
public struct SoundSettings
{
    public SoundType type;
    public AudioClip[] clips; 
}