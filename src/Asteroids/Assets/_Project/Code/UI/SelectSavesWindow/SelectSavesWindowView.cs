using System;
using _Project.Code.GameFlow.States.Menu;
using _Project.Code.Logic.Gameplay.Services.Holders.RepositoriesHolder;
using _Project.Code.Logic.Services.SaveLoad;
using Cysharp.Threading.Tasks;
using LitMotion;
using R3;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Project.Code.UI.SelectSavesWindow
{
    public class SelectSavesWindowView : MonoBehaviour
    {
        [SerializeField] private Button _localSaves;  
        [SerializeField] private Button _cloudSaves;

        [Header("AnimationSettings")]
        [SerializeField] private float _bounceScale = 1.2f;
        [SerializeField] private float _shakeStrength = 15f;
        [SerializeField]private float _shakeSpeed = 15f;
        [SerializeField] private float _durationMoveAnim;
        [SerializeField] private float _durationBounceUp;
        [SerializeField] private float _durationBounceDown;

        private MenuStateMachine _menuStateMachine;
        private IRepositoriesHolder _repositoriesHolder;
        private ISaveLoadService _saveLoadService;
        
        private RectTransform _rect;
        
        [Inject]
        public void Construct(MenuStateMachine menuStateMachine, ISaveLoadService saveLoadService)
        {
            _menuStateMachine = menuStateMachine;
            _saveLoadService = saveLoadService;
        }

        private void Awake() => 
            _rect = GetComponent<RectTransform>();

        private void Start()
        {
            _localSaves.OnClickAsObservable()
                .Subscribe(x => LoadLocalSaves())
                .AddTo(this);
        
            _cloudSaves.OnClickAsObservable()
                .Subscribe(x => LoadCloudSaves())
                .AddTo(this);
            
            PlayShowAnimation().Forget();
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
        
        private void LoadCloudSaves()
        {
            _saveLoadService.ResolveWithCloud();
            _menuStateMachine.Enter<MenuStartState>().Forget();
        }

        private void LoadLocalSaves()
        {
            _saveLoadService.ResolveWithLocal();
            _menuStateMachine.Enter<MenuStartState>().Forget();
        }
    
        public async UniTaskVoid Destroy()
        {
            await LMotion.Create(_rect.localScale, Vector3.zero, 0.5f)
                .WithEase(Ease.OutSine)
                .Bind(scale => _rect.localScale = scale)
                .AddTo(this)
                .ToUniTask();

            Destroy(gameObject);
        }
    }
}