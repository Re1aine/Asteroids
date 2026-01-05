using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.ReviveWindow
{
    public class Timer : MonoBehaviour
    {
        public Observable<Unit> Ended => _ended;

        [SerializeField] private Image _fill;
    
        private readonly Subject<Unit> _ended = new();

        CancellationTokenSource _tokenSource;
    
        private readonly float[] _pulseTimes = { 3f, 2f, 1f };

        private bool[] _triggeredPulses;
    
        private bool _isDecisionPicked;
    
        private float _duration;
    
        private void Awake()
        {
            _tokenSource = new CancellationTokenSource();
            _triggeredPulses = new bool[_pulseTimes.Length];
        }

        public void SetDuration(float duration)
        {
            _duration = duration;
        }
    
        public async void Run()
        {
            float elapsedTime = 0f;

            AnimatePulse(0.2f);

            while (elapsedTime < _duration && !_tokenSource.IsCancellationRequested)
            {
                var progress = elapsedTime / _duration;
                float remainingTime = _duration - elapsedTime;
            
                _fill.fillAmount = 1f - progress;
            
                TryAnimatePulse(remainingTime);
                
                elapsedTime += Time.deltaTime;

                await UniTask.Yield();
            }
        
            if(!_isDecisionPicked)
                _ended?.OnNext(Unit.Default);
        }

        private void TryAnimatePulse(float remainingTime)
        {
            for (int i = 0; i < _pulseTimes.Length; i++)
            {
                float pulseTime = _pulseTimes[i];
                if (remainingTime <= pulseTime && remainingTime > pulseTime - 0.1f && 
                    !_triggeredPulses[i])
                {
                    _triggeredPulses[i] = true;
                    AnimatePulse(0.2f);
                }
            }
        }

        private async void AnimatePulse(float duration)
        {
            await AnimateSizeUp(duration);
            await AnimateSizeDown(duration);
        }
    
        private async UniTask AnimateSizeUp(float duration)
        {
            var bigSize = transform.localScale * 1.5f; 
        
            float elapsedTime = 0;
            while (elapsedTime <= duration && !_tokenSource.IsCancellationRequested)
            {
                var progress = elapsedTime / duration;    
                transform.localScale = Vector3.Lerp(transform.localScale, bigSize, progress);
                elapsedTime += Time.deltaTime;
            
                await UniTask.Yield();
            }
            
            transform.localScale = bigSize;
        }

        private async UniTask AnimateSizeDown(float duration)
        {
            var normalSize = transform.localScale / 1.5f;
        
            float elapsedTime = 0;
            while (elapsedTime <= duration && !_tokenSource.IsCancellationRequested)
            {
                var progress = elapsedTime / duration;
                transform.localScale = Vector3.Lerp(transform.localScale, normalSize, progress);
                elapsedTime += Time.deltaTime;

                await UniTask.Yield();
            }
            
            transform.localScale = normalSize;
        }

        private void OnDestroy() => 
            _tokenSource.Cancel();

        public void Stop()
        {
            _isDecisionPicked = true;
            _tokenSource?.Cancel();
        }
    }
}