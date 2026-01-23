using System;
using _Project.Code.Logic.Gameplay.Entities.Player;
using _Project.Code.Logic.Gameplay.Projectiles.Bullet;
using _Project.Code.Logic.Gameplay.Services.Pause;
using _Project.Code.Logic.Gameplay.Services.Providers.PlayerProvider;
using UnityEngine;
using VContainer;

namespace _Project.Code.Logic.Gameplay.Entities.Enemy.UFO
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class UFOView : MonoBehaviour, IDamageable, IDamageDealer
    {
        public event Action<DamageType> OnDamageReceived;
        public DamageType DamageType => DamageType.UFO;
        public IDamageReceiver DamageReceiver { get; private set; }

        [SerializeField] private float _speed;

        private IPlayerProvider _playerProvider;
        private IPauseService _pauseService;

        Vector3 _direction;

        [Inject]
        public void Construct(IPlayerProvider playerProvider, IPauseService pauseService)
        {
            _playerProvider = playerProvider;
            _pauseService = pauseService;
        }

        public void Init(IDamageReceiver damageReceiver) => 
            DamageReceiver = damageReceiver;

        public void Configure(float speed)
        {
            _speed = speed;
        }
        
        private void Update()
        {
            if(_pauseService.IsPaused)
                return;
            
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
}