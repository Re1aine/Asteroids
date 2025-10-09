using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Logic.Gameplay.Services.Input
{
    public sealed class InputService : IInputService
    {
        private readonly PlayerInput _playerInput = new();
        public Vector2 Movement { get; private set; }
        public bool IsBulletShoot { get; private set; }
        public bool IsLaserShoot {get; private set;}
    
        public void Enable()
        {
            _playerInput.Gameplay.Move.performed += OnMove;
            _playerInput.Gameplay.Move.canceled += OnMove;

            _playerInput.Gameplay.LaserShoot.performed += OnLaserShoot;
            _playerInput.Gameplay.LaserShoot.canceled += OnLaserShoot;
        
            _playerInput.Gameplay.BulletShoot.performed += OnBulletShoot;
            _playerInput.Gameplay.BulletShoot.canceled += OnBulletShoot;
        
            _playerInput.Enable();
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            Movement = context.ReadValue<Vector2>();
        }

        private void OnLaserShoot(InputAction.CallbackContext context)
        {
            IsLaserShoot = context.ReadValueAsButton();
        }

        private void OnBulletShoot(InputAction.CallbackContext context)
        {
            IsBulletShoot = context.ReadValueAsButton();
        }
    
        public void Disable()
        {
            Movement = Vector2.zero;
        
            IsBulletShoot = false;
            IsLaserShoot = false;
        
            _playerInput.Gameplay.Move.performed -= OnMove;
            _playerInput.Gameplay.Move.canceled -= OnMove;

            _playerInput.Gameplay.LaserShoot.performed -= OnLaserShoot;
            _playerInput.Gameplay.LaserShoot.canceled -= OnLaserShoot;

            _playerInput.Gameplay.BulletShoot.performed -= OnBulletShoot;
            _playerInput.Gameplay.BulletShoot.canceled -= OnBulletShoot;

            _playerInput.Disable();
        }
    }
}