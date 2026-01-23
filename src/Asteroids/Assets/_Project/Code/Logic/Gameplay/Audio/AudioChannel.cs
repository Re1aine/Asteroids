using UnityEngine;

namespace _Project.Code.Logic.Gameplay.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioChannel : MonoBehaviour
    {
        [field: SerializeField] public SoundCategory SoundCategory { get; private set;}
        [field: SerializeField] public SoundType SoundType { get; private set;}
        
        private AudioSource _source;

        private bool _isPaused;
    
        private void Awake() => 
            _source = GetComponent<AudioSource>();

        private void Start() => 
            gameObject.name = $"[{gameObject.name}] : [{SoundType}]";

        public void Play(AudioClip clip)
        {
            if (_isPaused && _source.clip != null)
                UnPause();
            else
            {
                _source.clip = clip;
                _source.Play();
            }
        }

        public void Pause()
        {
            _isPaused = true;
            _source.Pause();
        }

        public void UnPause()
        {
            _isPaused = false;
            _source.UnPause();
        }

        public void Stop() => 
            _source.Stop();

        public void Reset() => 
            _isPaused = false;
    }
}