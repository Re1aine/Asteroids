using System;
using Code.Logic.Gameplay.Player;
using Code.Logic.Gameplay.Services;
using Code.Logic.Gameplay.Services.Boundries;
using UnityEngine;
using VContainer;

namespace Code.Logic.Gameplay.Asteroid
{
    public class AsteroidView : MonoBehaviour, IDamageable, IDamageDealer
    {
        public event Action<DamageType> OnDamageReceived;
        public virtual DamageType DamageType => DamageType.Asteroid;
        public IDamageReceiver DamageReceiver { get; private set; }

        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private float _speed;
        [field: SerializeField] public AsteroidType AsteroidType {get; private set;}

        private IBoundaries _boundaries;

        Vector3 _direction;
    
        [Inject]
        public void Construct(IBoundaries boundaries)
        {
            _boundaries = boundaries;
        }

        public void Init(IDamageReceiver damageReceiver)
        {
            DamageReceiver = damageReceiver;
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
            if (other.gameObject.TryGetComponent(out Bullet.Bullet bullet)) 
                OnDamageReceived?.Invoke(DamageType.Bullet); 
        }
    }
}