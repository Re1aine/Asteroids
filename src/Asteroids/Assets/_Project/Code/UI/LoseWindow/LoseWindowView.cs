using _Project.Code.GameFlow.States.Core;
using _Project.Code.GameFlow.States.Gameplay;
using _Project.Code.Infrastructure.Common.SceneLoader;
using Cysharp.Threading.Tasks;
using LitMotion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Project.Code.UI.LoseWindow
{
    public class LoseWindowView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _currentScore;
        [SerializeField] private TextMeshProUGUI _highScore;
        [SerializeField] private Button _restart; 
        [SerializeField] private Button _menu;

        [Header("AnimationSettings")]
        [SerializeField] private float _bounceScale = 1.2f;
        [SerializeField] private float _shakeStrength = 15f;
        [SerializeField]private float _shakeSpeed = 15f;
        [SerializeField] private float _durationMoveAnim;
        [SerializeField] private float _durationBounceUp;
        [SerializeField] private float _durationBounceDown;
        
        private GameplayStateMachine _gameplayStateMachine;

        private RectTransform _rect;
        
        [Inject]
        public void Construct(GameplayStateMachine gameplayStateMachine)
        {
            _gameplayStateMachine = gameplayStateMachine;
        }

        private void Awake() => 
            _rect = GetComponent<RectTransform>();
        
        private void Start()
        {
            _restart.onClick.AddListener(() => _gameplayStateMachine.Enter<GameplayInitState>().Forget());
            _menu.onClick.AddListener(() => _gameplayStateMachine.Enter<LoadSceneState, GameScenes>(GameScenes.Menu).Forget());
            
            PlayShowAnimation().Forget();
        }

        private async UniTaskVoid PlayShowAnimation()
        {
            await PlayEntranceAnimation();
            PlayConstantShake();
        }

        public void SetScore(int value) =>
            _currentScore.text = value.ToString();

        public void SetHighScore(int value) =>
            _highScore.text = value.ToString(); 
        
        public void Destroy() =>
            Destroy(gameObject);
        
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