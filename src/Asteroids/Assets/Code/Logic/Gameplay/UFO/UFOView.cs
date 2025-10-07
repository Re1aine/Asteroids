using System;
using UnityEngine;
using VContainer;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class UFOView : ViewBase, IDamageable, IDamageDealer
{
    public DamageType DamageType => DamageType.UFO;
    public IDamageReceiver DamageReceiver { get; private set; }

    [SerializeField] private float _speed;

    private IPlayerProvider _playerProvider;

    UFOPresenter _presenter;

    Vector3 _direction;


    [Inject]
    public void Construct(IPlayerProvider playerProvider)
    {
        _playerProvider = playerProvider;
    }

    private void Start()
    {
        DamageReceiver = _presenter.DamageReceiver;
    }

    public void Init(UFOPresenter presenter)
    {
        _presenter = presenter;
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
            _presenter.ReceiveDamage(DamageType.Bullet);
    }
}