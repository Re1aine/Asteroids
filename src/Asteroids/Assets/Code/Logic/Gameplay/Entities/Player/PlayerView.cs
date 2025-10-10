using System;
using Code.Logic.Gameplay.Services.Input;
using UnityEngine;
using VContainer;

namespace Code.Logic.Gameplay.Entities.Player
{
    public class PlayerView : MonoBehaviour
    {
        public event Action<DamageType>  OnDamageReceived;
    
        [SerializeField] private Rigidbody2D _rigidbody;

        [SerializeField] private Gun _gun;
    
        [SerializeField] private float _decelerationMove;
        [SerializeField] private float _accelerationMove;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotateSpeed;

        private IInputService _inputService;

        private float _rotateAngle;

        private Vector2 _moveDirection;
        private Vector2 _velocity;

        [Inject]
        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }
    
        public void SetMoveDirection(Vector3 direction) => 
            _moveDirection = direction;

        public float GetVelocity() => _velocity.magnitude;

        public float GetLaserCooldown() => _gun.GetLaserCooldownTimer();

        public int GetLaserCharges() => _gun.GetLaserChargesCount();

        private void Start()
        {
            _velocity = Vector2.zero;
            _rigidbody.linearVelocity = Vector2.zero;
            _moveDirection = Vector2.zero;
        }

        private void Update()
        {
            _moveDirection = _inputService.Movement;
        
            HandleRotate();
        }
    
        private void FixedUpdate() => HandleMove();

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
        
            _rigidbody.linearVelocity = _velocity;
        }
    
        private void AccelerateMove()
        {
            Vector2 targetVelocity = (Vector2)transform.up * (_moveDirection.y * _moveSpeed);
            _velocity = Vector2.Lerp(_velocity, targetVelocity, _accelerationMove * Time.fixedDeltaTime);
        }
    
        private void DecelerateMove() => 
            _velocity = Vector2.Lerp(_velocity, Vector2.zero, _decelerationMove  * Time.fixedDeltaTime);
    
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out IDamageDealer damageDealer)) 
                OnDamageReceived?.Invoke(damageDealer.DamageType);
        }
    }
}