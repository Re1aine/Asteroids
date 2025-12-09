using System;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioSFX", menuName = "AudioSFX")]
public class AudioConfig : ScriptableObject
{
    public SoundSettingsVo[] SFXs;
}

public enum SFXType
{
    BulletShoot = 0,
    LaserShoot = 1,
    Music = 2
}


[Serializable]
public struct SoundSettingsVo
{
    public SFXType type;
    public AudioClip[] clips; 
}