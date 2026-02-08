using _Project.Code.GameFlow.States.Core;
using _Project.Code.GameFlow.States.Menu;
using _Project.Code.Infrastructure.Common.SceneLoader;
using _Project.Code.Logic.Services.Authentification;
using Cysharp.Threading.Tasks;
using LitMotion;
using R3;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Project.Code.UI.MenuWindow
{
    public class MenuWindowView : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Transform _menuWord;
        [SerializeField] private Transform _menuWordTargetPos;

        [Header("AnimationSettings")]
        [SerializeField] private float _bounceScale = 1.2f;
        [SerializeField] private float _shakeStrength = 15f;
        [SerializeField] private float _shakeSpeed = 15f;
        [SerializeField] private float _durationMoveAnim;
        [SerializeField] private float _durationBounceUp;
        [SerializeField] private float _durationBounceDown;
        
        private MenuStateMachine _menuStateMachine;
        private IAuthentification _authentification;

        private RectTransform _rect;
        private RectTransform _menuWordRect;

        private Vector2 _originPosition;
        private Vector2 _targetPosition;
        private Vector2 _menuWordOriginPosition;
        
        [Inject]
        public void Construct(MenuStateMachine menuStateMachine)
        {
            _menuStateMachine =  menuStateMachine;
        }

        private void Awake()
        {
            _rect = GetComponent<RectTransform>();
            _menuWordRect = _menuWord.GetComponent<RectTransform>();
            
            _originPosition = _rect.anchoredPosition;
            _targetPosition = Vector2.zero;
            
            _menuWordOriginPosition = _menuWordRect.anchoredPosition;
        }

        private void Start()
        {
            _playButton.OnClickAsObservable()
                .Subscribe(_ => _menuStateMachine.Enter<LoadSceneState, GameScenes>(GameScenes.Gameplay).Forget())
                .AddTo(this);;

            _exitButton.OnClickAsObservable()
                .Subscribe(_ => _menuStateMachine.Enter<ExitGameState>().Forget())
                .AddTo(this);;

            PlayShowAnimation().Forget();
        }

        private async UniTaskVoid PlayShowAnimation()
        {
            await PlayEntranceAnimation();
            PlayConstantShake();
        }

        private UniTask PlayEntranceAnimation()
        {
            var sequence = LSequence.Create();

            sequence.Append(
                LMotion.Create(_originPosition, _targetPosition, _durationMoveAnim)
                    .WithDelay(0.5f)
                    .WithEase(Ease.OutBack)
                    .Bind(pos => _rect.anchoredPosition = pos)
                    .AddTo(this)
            );

            sequence.Join(
                LMotion.Create(_rect.localScale, Vector3.one * _bounceScale, _durationBounceUp)
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

            sequence.Append(
                LMotion.Create(_menuWordOriginPosition, _menuWordTargetPos.GetComponent<RectTransform>().anchoredPosition, 0.2f)
                    .WithEase(Ease.OutSine)
                    .Bind(pos => _menuWordRect.anchoredPosition = pos)
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
        
        public void Destroy() => 
            Destroy(gameObject);
    }
}