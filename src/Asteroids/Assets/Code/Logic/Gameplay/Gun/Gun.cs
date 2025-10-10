using System.Collections;
using Code.Logic.Gameplay.Entities;
using Code.Logic.Gameplay.Projectiles.Bullet;
using Code.Logic.Gameplay.Projectiles.LaserBeam;
using Code.Logic.Gameplay.Services.Factories.GameFactory;
using Code.Logic.Gameplay.Services.Input;
using Code.Logic.Gameplay.Services.Providers.PlayerProvider;
using UnityEngine;
using VContainer;

namespace Code.Logic.Gameplay
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private Transform _shootPoint;
    
        [Header("Bullet")]
        [SerializeField] private float _bulletCooldown;

        [Header("LaserBeam")]
        [SerializeField] private LayerMask _obstacleLayer;

        [SerializeField] private float _laserShootCooldown;
        [SerializeField] private float _laserShootTime;
        [SerializeField] private float _laserRange;
        [SerializeField] private int _laserChargesMax;
        [SerializeField] public int _laserChargesCurrent;
        [SerializeField] private int _laserChargeRefillCooldown;

        private IGameFactory _gameFactory;
        private IInputService _inputService;
        private IPlayerProvider _playerProvider;

        private Vector3 _shootDirection;
    
        private float _laserShootCooldownTimer;
        private float _laserShootTimer;
        private float _laserChargeRefillTimer;
        private float _bulletTimer;

        private bool _isLaserActive;
    
        private LaserBeam _laserBeam;

        [Inject]
        public void Construct(IInputService inputService, IGameFactory gameFactory, IPlayerProvider playerProvider)
        {
            _inputService = inputService;
            _gameFactory = gameFactory;
            _playerProvider = playerProvider;
        }

        private void Awake() => 
            _shootDirection = (_shootPoint.position -  transform.position);
    
        private void Update()
        {
            _shootDirection = _shootPoint.position -  transform.position;
        
            HandleShoot();
            HandleLaserShoot();
        }
    
        public int GetLaserChargesCount() => _laserChargesCurrent;
        public float GetLaserCooldownTimer() => _laserShootCooldownTimer; 
    
        private IEnumerator ShootLaserRoutine()
        {
            _isLaserActive = true;
            _laserShootTimer = _laserShootTime;
        
            _laserBeam = _gameFactory.CreateLaserBeam(_shootPoint.position, RotateHelper.GetRotation2D(_shootDirection)); 
        
            while (_inputService.IsLaserShoot && _laserShootTimer > 0)
            {
                _laserShootTimer -= Time.deltaTime;
            
                _playerProvider.Player.View.SetMoveDirection(new Vector3(_inputService.Movement.x, 0, 0));

                Vector3 direction = (_shootPoint.position - transform.position).normalized;
                Vector3 endPos = _shootPoint.position + direction * _laserRange;

                RaycastHit2D hit = Physics2D.Raycast(_shootPoint.position, direction, _laserRange, _obstacleLayer);

                if (hit.collider != null)
                {
                    if(hit.collider.TryGetComponent(out IDamageable damageable))
                        damageable.ReceiveDamage(DamageType.LaserBeam);
                } 
           
                _laserBeam.LineRenderer.SetPosition(0, _shootPoint.position);
                _laserBeam.LineRenderer.SetPosition(1, endPos); 
                yield return null; 
            }
        
            _isLaserActive = false;
            _laserChargesCurrent--;
        
            Destroy(_laserBeam.gameObject);

            StartCoroutine(CountLaserShootCooldownTimer());
        }
    
        private void HandleShoot()
        {
            _bulletTimer -= Time.deltaTime;
        
            if (!IsCanShoot()) return;
            Shoot();
            _bulletTimer = _bulletCooldown;
        }

        private void HandleLaserShoot()
        {
            if (IsCanLaserShoot()) 
                StartCoroutine(ShootLaserRoutine());
        
            RefillLaserCharge();
        }

        private IEnumerator CountLaserShootCooldownTimer()
        {
            _laserShootCooldownTimer = _laserShootCooldown;
        
            while (_laserShootCooldownTimer >= 0)
            {
                _laserShootCooldownTimer -= Time.deltaTime;
                yield return null;
            }
        
            _laserShootCooldownTimer = 0; 
        }

        private bool IsCanLaserShoot() =>
            _inputService.IsLaserShoot && !_isLaserActive && _laserChargesCurrent > 0 && _laserShootCooldownTimer <= 0;

        private void RefillLaserCharge()
        {
            _laserChargeRefillTimer -= Time.deltaTime;

            if (_laserChargeRefillTimer <= 0 && _laserChargesCurrent < _laserChargesMax)
            {
                _laserChargesCurrent++;
                _laserChargeRefillTimer = _laserChargeRefillCooldown;
            }
        }

        private bool IsCanShoot() => 
            _bulletTimer <= 0 && _inputService.IsBulletShoot;
    
        private void Shoot()
        {
            Bullet bullet = _gameFactory.CreateBullet(_shootPoint.position, RotateHelper.GetRotation2D(_shootDirection));
            bullet.MoveToDirection(transform.up);
        }

        private void OnDestroy()
        {
            if (_laserBeam != null)
                _laserBeam.ClearAllLinePositions();
        }
    }
}