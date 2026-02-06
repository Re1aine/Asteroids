using Cysharp.Threading.Tasks;
using LitMotion;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Code.UI.ReviveWindow
{
    public class ReviveWindowView : MonoBehaviour
    {
        public Observable<Unit> Accepted => _accepted;
        public Observable<Unit> Declined => _declined;
    
        private readonly Subject<Unit> _accepted = new();
        private readonly Subject<Unit> _declined = new();
    
        [SerializeField] private Timer _timer;
        [SerializeField] private Button _declineButton;
        [SerializeField] private Button _acceptButton;
        
        [Header("AnimationSettings")]
        [SerializeField] private float _bounceScale = 1.2f;
        [SerializeField] private float _shakeStrength = 15f;
        [SerializeField] private float _shakeSpeed = 15f;
        [SerializeField] private float _durationMoveAnim;
        [SerializeField] private float _durationBounceUp;
        [SerializeField] private float _durationBounceDown;
        
        private RectTransform _rect;
    
        private void Awake()
        {
            _acceptButton.OnClickAsObservable()
                .Subscribe(_ => _accepted.OnNext(Unit.Default))
                .AddTo(this);

            _declineButton.OnClickAsObservable()
                .Subscribe(_ => _declined.OnNext(Unit.Default))
                .AddTo(this);
        
            _timer.Ended
                .Subscribe(_ => _declined.OnNext(Unit.Default))
                .AddTo(this);

            _rect = GetComponent<RectTransform>();
        }

        private void Start() => 
            PlayShowAnimation().Forget();

        public void SetActiveTimer(bool isActive)
        {
            if (isActive)
                _timer.Run().Forget();
            else
                _timer.Stop();
        }

        public void SetTimerDuration(float duration) => 
            _timer.SetDuration(duration);

        public async UniTaskVoid Destroy()
        {
            await LMotion.Create(_rect.localScale, Vector3.zero, 0.5f)
                .WithEase(Ease.OutSine)
                .Bind(scale => _rect.localScale = scale)
                .AddTo(this)
                .ToUniTask();

            Destroy(gameObject);
        }

        private async UniTaskVoid PlayShowAnimation()
        {
            await PlayEntranceAnimation();
            PlayConstantShake();
        }

        private UniTask PlayEntranceAnimation()
        {
            Vector2 startPos = _rect.anchoredPosition;

            var sequence = LSequence.Create();

            sequence.Append(
                LMotion.Create(startPos, Vector2.zero, _durationMoveAnim)
                    .WithEase(Ease.OutBack)
                    .Bind(pos => _rect.anchoredPosition = pos)
                    .AddTo(this)
            );

            _rect.localScale = Vector3.zero;
            sequence.Join(
                LMotion.Create(Vector3.zero, Vector3.one * _bounceScale, _durationBounceUp)
                    .WithEase(Ease.OutBack)
                    .Bind(scale => _rect.localScale = scale)
                    .AddTo(this)
            );
        
            sequence.Append(
                LMotion.Create(Vector3.one * _bounceScale, Vector3.one, _durationBounceDown)
                    .WithEase(Ease.OutSine)
                    .Bind(scale => _rect.localScale = scale)
                    .AddTo(this)
            );
            
            return sequence.Run().ToUniTask();
        }
        
        private void PlayConstantShake()
        {
            LMotion.Create(0f, 1f, 0.5f)
                .WithLoops(-1)
                .Bind(t => {
                    float shakeX = Mathf.Sin(Time.time * _shakeSpeed) * _shakeStrength;
                    float shakeY = Mathf.Cos(Time.time * _shakeSpeed * 0.7f) * (_shakeStrength * 0.5f);
            
                    float strengthVariation = 1f + Mathf.Sin(Time.time * 0.5f) * 0.3f;
            
                    _rect.anchoredPosition = Vector2.zero + new Vector2(shakeX, shakeY) * strengthVariation * 0.1f;
                })
                .AddTo(this);
        }
    }
}