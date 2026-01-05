using System;
using Code.Logic.Gameplay.Entities.Player;
using Code.Logic.Gameplay.Projectiles.Bullet;
using Code.Logic.Gameplay.Services.Boundries;
using Code.Logic.Gameplay.Services.Pause;
using UnityEngine;
using VContainer;

namespace Code.Logic.Gameplay.Entities.Enemy.Asteroid
{
    public class AsteroidView : MonoBehaviour, IDamageable, IDamageDealer
    {
        public event Action<DamageType> OnDamageReceived;
        public virtual DamageType DamageType => DamageType.Asteroid;
        public IDamageReceiver DamageReceiver { get; private set; }

        [field: SerializeField] public AsteroidType AsteroidType {get; private set;}

        [SerializeField] private float _speed;

        private IBoundaries _boundaries;
        private IPauseService _pauseService;
        
        Vector3 _direction;

        [Inject]
        public void Construct(IBoundaries boundaries, IPauseService pauseService)
        {
            _boundaries = boundaries;
            _pauseService = pauseService;
        }
        
        public void Configure(float speed)
        {
            _speed = speed;
        }
        
        public void Init(IDamageReceiver damageReceiver) => 
            DamageReceiver = damageReceiver;
        
        private void Update()
        {
            if(_pauseService.IsPaused)
                return;
            
            Move();
        }
        
        public void SetSpeed(float speed) => 
            _speed = speed;
        
        public void SetDirection(Vector3 direction) => 
            _direction = direction;

        public void SetRandomDirection()
        {
            Vector3 randomPos = RandomHelper.GetRandomPositionInsideBounds(_boundaries.Min, _boundaries.Max);
            _direction = (randomPos - transform.position).normalized;
        }
        
        private void Move() => 
            transform.position += _direction * _speed * Time.deltaTime;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out Bullet bullet)) 
                OnDamageReceived?.Invoke(DamageType.Bullet); 
        }
    }
}