using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioChannel : MonoBehaviour
{
    public SFXType SfxType => _sfxType;
    
    [field: SerializeField] public AudioChannelType AudioChannelType { get; private set;}
    
    private AudioSource _source;
    private SFXType _sfxType;
    
    private void Awake() => 
        _source = GetComponent<AudioSource>();

    public void Play(SFXType type, AudioClip clip)
    {
        _sfxType = type;
        _source.clip = clip;
        _source.Play();
    }

    public void Stop() => 
        _source.Stop();
}

public enum AudioChannelType
{
    ShortSounds = 1,
    Music = 2  
}