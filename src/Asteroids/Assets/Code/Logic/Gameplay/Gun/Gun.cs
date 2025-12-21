using System;
using System.Collections;
using Code.Logic.Gameplay.Entities;
using Code.Logic.Gameplay.Projectiles.Bullet;
using Code.Logic.Gameplay.Projectiles.LaserBeam;
using Code.Logic.Gameplay.Services.Factories.GameFactory;
using Code.Logic.Gameplay.Services.Input;
using Code.Logic.Gameplay.Services.Providers.PlayerProvider;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using VContainer;

namespace Code.Logic.Gameplay.Gun
{
    public class Gun : MonoBehaviour
    {
        public event Action BulletShoot;
        public event Action LaserShootStarted;
        public event Action LaserShootEnded;
        
        [SerializeField] private Transform _shootPoint;
    
        [Header("Bullet")]
        [SerializeField] private float _bulletCooldown;

        [Header("LaserBeam")]
        [SerializeField] private LayerMask _obstacleLayer;
        [SerializeField] private float _laserShootCooldown;
        [SerializeField] private float _laserShootTime;
        [SerializeField] private float _laserRange;
        [SerializeField] private int _laserChargesMax;
        [SerializeField] private int _laserChargesCurrent;
        [SerializeField] private int _laserChargeRefillCooldown;

        private IGameFactory _gameFactory;
        private IInputService _inputService;
        private IPlayerProvider _playerProvider;

        private LaserBeam _laserBeam;

        public ReadOnlyReactiveProperty<int> LaserCharges;
        public ReadOnlyReactiveProperty<float> CooldownLaser;
        
        private ReactiveProperty<int> _laserCharges;
        private ReactiveProperty<float> _cooldownLaser;

        private readonly CompositeDisposable _disposables = new();
        
        private Vector3 _shootDirection;

        private float _laserShootCooldownTimer;
        private float _laserShootTimer;
        private float _laserChargeRefillTimer;
        private float _bulletTimer;

        private bool _isLaserActive;
        
        [Inject]
        public void Construct(IInputService inputService, IGameFactory gameFactory, IPlayerProvider playerProvider)
        {
            _inputService = inputService;
            _gameFactory = gameFactory;
            _playerProvider = playerProvider;
        }

        private void Awake()
        {
            _shootDirection = (_shootPoint.position - transform.position);

            _laserCharges = new ReactiveProperty<int>(_laserChargesCurrent);
            _cooldownLaser = new ReactiveProperty<float>(_laserShootCooldownTimer);

            LaserCharges = _laserCharges.ToReadOnlyReactiveProperty();
            CooldownLaser = _cooldownLaser.ToReadOnlyReactiveProperty();

            Observable.EveryValueChanged(this, x => _laserChargesCurrent)
                .DistinctUntilChanged()
                .Subscribe(x => _laserCharges.Value = _laserChargesCurrent)
                .AddTo(_disposables);
         
            Observable.EveryValueChanged(this, x => _laserShootCooldownTimer)
                .DistinctUntilChanged()
                .Subscribe(x => _cooldownLaser.Value = _laserShootCooldownTimer)
                .AddTo(_disposables);
        }

        private void Update()
        {
            _shootDirection = _shootPoint.position -  transform.position;
        
            HandleShoot();
            HandleLaserShoot();
        }
        
        private IEnumerator ShootLaserRoutine()
        {
            LaserShootStarted?.Invoke();
            
            _isLaserActive = true;
            _laserShootTimer = _laserShootTime;
            
             UniTask<LaserBeam> laserBeam = _gameFactory.CreateLaserBeam(_shootPoint.position, RotateHelper.GetRotation2D(_shootDirection)); 
             
             yield return UniTask.WaitUntil(() => laserBeam.Status == UniTaskStatus.Succeeded).ToCoroutine();
             
             _laserBeam = laserBeam.GetAwaiter().GetResult();
             
             _playerProvider.Player.View.LockForwardMovement();
            
             while (_inputService.IsLaserShoot && _laserShootTimer > 0)
             {
                 _laserShootTimer -= Time.deltaTime;
             
                 //_playerProvider.Player.View.SetMoveDirection(new Vector3(_inputService.Movement.x, 0, 0));
        
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
            
            //_playerProvider.Player.View.IsMovementLocked = false;
            _playerProvider.Player.View.UnLockForwardMovement();
            
            LaserShootEnded?.Invoke();
            
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
    
        private async void Shoot()
        {
            Bullet bullet = await _gameFactory.CreateBullet(_shootPoint.position, RotateHelper.GetRotation2D(_shootDirection));
            bullet.SetDirection(transform.up);
            
            BulletShoot?.Invoke();
        }

        private void OnDestroy()
        {
            if (_laserBeam != null)
                _laserBeam.ClearAllLinePositions();
            
            _disposables.Dispose();
        }
    }
}