using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioChannel : MonoBehaviour
{
    [field: SerializeField] public SoundCategory SoundCategory { get; private set;}
    [field: SerializeField] public SoundType SoundType { get; private set;}
        
    private AudioSource _source;
    
    private void Awake() => 
        _source = GetComponent<AudioSource>();

    public void Play(AudioClip clip)
    {
        _source.clip = clip;
        _source.Play();
    }

    public void Stop() => 
        _source.Stop();
}