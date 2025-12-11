using System;
using Code.Logic.Gameplay.Services.Input;
using R3;
using UnityEngine;
using VContainer;

namespace Code.Logic.Gameplay.Entities.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerView : MonoBehaviour
    {
        public event Action<DamageType>  OnDamageReceived;
        
        [field: SerializeField] public Gun Gun {get; private set; }

        [SerializeField] private float _decelerationMove;
        [SerializeField] private float _accelerationMove;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotateSpeed;

        private IInputService _inputService;

        private Rigidbody2D _rigidbody;

        public ReadOnlyReactiveProperty<Vector3> Position => _position;
        public ReadOnlyReactiveProperty<Quaternion> Rotation => _rotation;
        public ReadOnlyReactiveProperty<float> Velocity => _velocity;
        
        private ReactiveProperty<Vector3> _position;
        private ReactiveProperty<Quaternion> _rotation;
        private ReactiveProperty<float> _velocity;
        
        private readonly CompositeDisposable _disposables = new();
        
        private Vector2 _moveDirection;
        private Vector2 _currentVelocity;

        private float _rotateAngle;

        [Inject]
        public void Construct(IInputService inputService) => 
            _inputService = inputService;

        public void SetMoveDirection(Vector3 direction) => 
            _moveDirection = direction;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();

            _position = new ReactiveProperty<Vector3>();
            _rotation = new ReactiveProperty<Quaternion>();
            _velocity =  new ReactiveProperty<float>();
            
            Observable.EveryValueChanged(this, x => transform.position)
                .DistinctUntilChanged()
                .Subscribe(position=> _position.Value = position)
                .AddTo(_disposables);
            
            Observable.EveryValueChanged(this,  x => transform.rotation)
                .DistinctUntilChanged()
                .Subscribe(rotation=> _rotation.Value = rotation)
                .AddTo(_disposables);
            
            Observable.EveryValueChanged(this, x => _currentVelocity.magnitude)
                .DistinctUntilChanged()
                .Subscribe(velocity=> _velocity.Value = velocity)
                .AddTo(_disposables);
        }

        private void Start()
        {
            _currentVelocity = Vector2.zero;
            _rigidbody.linearVelocity = Vector2.zero;
            _moveDirection = Vector2.zero;
        }

        private void Update()
        {
            _moveDirection = _inputService.Movement;
        
            HandleRotate();
        }
    
        private void FixedUpdate() =>
            HandleMove();

        private void HandleRotate()
        {
            _rotateAngle -= _inputService.Movement.x * _rotateSpeed;
            transform.rotation = Quaternion.Euler(0, 0, _rotateAngle);
        }
    
        private void HandleMove()
        {
            if (_moveDirection.y != 0)
                AccelerateMove();
            else 
                DecelerateMove();
        
            _rigidbody.linearVelocity = _currentVelocity;
        }
    
        private void AccelerateMove()
        {
            Vector2 targetVelocity = (Vector2)transform.up * (_moveDirection.y * _moveSpeed);
            _currentVelocity = Vector2.Lerp(_currentVelocity, targetVelocity, _accelerationMove * Time.fixedDeltaTime);
        }
    
        private void DecelerateMove() => 
            _currentVelocity = Vector2.Lerp(_currentVelocity, Vector2.zero, _decelerationMove  * Time.fixedDeltaTime);
    
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out IDamageDealer damageDealer)) 
                OnDamageReceived?.Invoke(damageDealer.DamageType);
        }

        private void OnDestroy() => 
            _disposables.Dispose();
    }
}