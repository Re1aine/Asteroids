using System.Threading;
using Code.Logic.Gameplay.Entities;
using Code.Logic.Gameplay.Services.Providers.PlayerProvider;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

public class ShockWaveEffector : MonoBehaviour
{
    private static readonly int WaveDistanceFromCenter = Shader.PropertyToID("_WaveDistanceFromCenter");
    
    [SerializeField] private GameObject _shockWaveEffect;
    [SerializeField] private float _durationEffect = 1f;
    [SerializeField] private float _maxRadius = 5f;

    [SerializeField] private bool _drawGizmos;

    [SerializeField] private AnimationCurve _shaderCurve;
    [SerializeField] private AnimationCurve _colliderCurve;

    [SerializeField] private LayerMask _layerMask;

    private IPlayerProvider _playerProvider;
    
    private Material _material;

    private CancellationTokenSource _tokenSource;
    
    private bool _isActive;

    private float _currentRadius;
    
    [Inject]
    public void Construct(IPlayerProvider playerProvider) => 
        _playerProvider = playerProvider;

    private void Awake()
    {
        _material = _shockWaveEffect.GetComponent<Renderer>()?.material;
        _shockWaveEffect.SetActive(false);
    }
    
    public async UniTask CreateShockWave()
    {
        if (_isActive)
            return;

        _isActive = true;
        
        transform.position = _playerProvider.Player.View.transform.position;
        _tokenSource = new CancellationTokenSource();
        
        _shockWaveEffect.SetActive(true);
        
        _material.SetFloat(WaveDistanceFromCenter, -0.1f);
        
        float elapsedTime = 0;
        while (elapsedTime < _durationEffect && !_tokenSource.IsCancellationRequested)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / _durationEffect;
    
            UpdateShockWave(progress);
            await UniTask.Yield();
        }
        
        Cleanup();
    }
    
    public void StopShockWave() =>
        Cleanup();

    private void UpdateShockWave(float progress)
    {
        float shaderProgress = _shaderCurve.Evaluate(progress);
        float shaderValue = Mathf.Lerp(-0.1f, 1f, shaderProgress);
        _material.SetFloat(WaveDistanceFromCenter, shaderValue);
        
        float colliderProgress = _colliderCurve.Evaluate(progress);
        _currentRadius = Mathf.Lerp(0f, _maxRadius, colliderProgress);
        
        CheckCollisions();
    }

    private void CheckCollisions()
    {
        var enemies = Physics2D.OverlapCircleAll(transform.position, _currentRadius, _layerMask);
        
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i]
                .GetComponent<IDamageable>()
                .ReceiveDamage(DamageType.LaserBeam);
        }
    }
    
    private void Cleanup()
    {
        _shockWaveEffect.SetActive(false);
        _tokenSource?.Cancel();
        _isActive = false;
    }
    
    private void OnDrawGizmosSelected()
    {
        if (!_drawGizmos)
            return;

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, _currentRadius);
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _maxRadius);
        
        if (_isActive)
        {
            Gizmos.color = new Color(1f, 0f, 0f, 0.2f);
            Gizmos.DrawSphere(transform.position, _currentRadius);
        }
    }
    
    private void OnDestroy() => 
        Cleanup();
}