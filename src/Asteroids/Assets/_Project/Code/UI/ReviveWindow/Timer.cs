using System.Threading;
using Cysharp.Threading.Tasks;
using LitMotion;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Code.UI.ReviveWindow
{
    public class Timer : MonoBehaviour
    {
        public Observable<Unit> Ended => _ended;
        private readonly Subject<Unit> _ended = new();

        [SerializeField] private Image _fill;
        [SerializeField] private Image _frame;
        
        [Header("AnimationSettings")]
        [SerializeField] private float _durationBounce;
        [SerializeField] private float _bounceScale;
        [SerializeField] private float _pulseStepSpeed = 0.005f;

        private RectTransform _rect;
        
        private CancellationTokenSource _cts;

        private MotionHandle _handlePulse;
        
        private float _duration;
        
        private bool _isDecisionPicked;

        private void Awake()
        {
            _rect = GetComponent<RectTransform>();
            _cts = new CancellationTokenSource();   
        }

        public void SetDuration(float duration) => 
            _duration = duration;
        
        public async UniTaskVoid Run()
        {
            _handlePulse = AnimatePulse();
            
            float elapsedTime = 0;
            
            while (elapsedTime < _duration && !_cts.IsCancellationRequested)
            {
                var progress = elapsedTime / _duration;
                
                _fill.fillAmount = 1f - progress;

                _fill.color = Color.Lerp(Color.cyan, Color.red, progress);
                _frame.color = Color.Lerp(Color.cyan, Color.red, progress);
                
                _handlePulse.PlaybackSpeed += _pulseStepSpeed;
                
                elapsedTime += Time.deltaTime;
            
                await UniTask.Yield(PlayerLoopTiming.Update, _cts.Token);
            }
            
            if(!_isDecisionPicked)
                _ended.OnNext(Unit.Default);
        }
        
        private MotionHandle AnimatePulse()
        {
            return LMotion.Create(_rect.localScale, Vector3.one * _bounceScale, _durationBounce)
                .WithEase(Ease.OutSine)
                .WithLoops(-1, LoopType.Yoyo)
                .Bind(scale => _rect.localScale = scale)
                .AddTo(this);
        }

        public void Stop() => 
            _isDecisionPicked = true;

        private void OnDestroy() =>
            _cts.Cancel();
    }
}