using System;
using UnityEngine;
using VContainer;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class UFOView : MonoBehaviour, IDamageable, IDamageDealer
{
    public event Action<DamageType> OnDamageReceived;
    public DamageType DamageType => DamageType.UFO;
    public IDamageReceiver DamageReceiver { get; private set; }

    [SerializeField] private float _speed;

    private IPlayerProvider _playerProvider;
    
    Vector3 _direction;
    
    [Inject]
    public void Construct(IPlayerProvider playerProvider)
    {
        _playerProvider = playerProvider;
    }
    
    public void Init(IDamageReceiver damageReceiver)
    {
        DamageReceiver = damageReceiver;
    }
    
    private void Update()
    {
        MoveToPlayer();
    }

    private void MoveToPlayer()
    {
        if (_playerProvider.Player?.View == null)
            return;
        
        _direction = (_playerProvider.Player.View.transform.position - transform.position).normalized;
        transform.position += _direction * _speed * Time.deltaTime; 
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out Bullet bullet)) 
            OnDamageReceived?.Invoke(DamageType.Bullet);
    }
}