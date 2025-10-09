using System;
using UnityEngine;
using VContainer;

public class AsteroidView : MonoBehaviour, IDamageable, IDamageDealer
{
    public virtual DamageType DamageType => DamageType.Asteroid;
    public IDamageReceiver DamageReceiver { get; private set; }

    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _speed;
    [field: SerializeField] public AsteroidType AsteroidType {get; private set;}

    private AsteroidPresenter _presenter;

    private IBoundaries _boundaries;

    Vector3 _direction;


    [Inject]
    public void Construct(IBoundaries boundaries)
    {
        _boundaries = boundaries;
    }

    public void Init(AsteroidPresenter presenter)
    {
        _presenter = presenter;
    }

    private void Start()
    {
        DamageReceiver = _presenter.DamageReceiver;
    }

    public void LaunchInDirection(Vector3 direction)
    {
        _rigidbody2D.AddForce(direction * _speed, ForceMode2D.Impulse);        
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

    public void LaunchInRandomDirection()
    {
        Vector3 randomPos = RandomHelper.GetRandomPositionInsideBounds(_boundaries.Min, _boundaries.Max);
        _direction = (randomPos - transform.position).normalized;
        _rigidbody2D.AddForce(_direction * _speed, ForceMode2D.Impulse);        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out Bullet bullet)) 
            _presenter.ReceiveDamage(DamageType.Bullet);
    }
}