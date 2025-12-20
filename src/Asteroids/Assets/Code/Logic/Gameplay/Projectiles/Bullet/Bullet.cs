using System;
using UnityEngine;
using VContainer;

namespace Code.Logic.Gameplay.Projectiles.Bullet
{
    [RequireComponent(typeof(CapsuleCollider2D))]
    public class Bullet : MonoBehaviour
    { 
        public event Action<Bullet> Destroyed;

        [SerializeField] private float _speed;
        
        private IPauseService _pauseService;
        
        private Vector3 _direction;
        
        [Inject]
        public void Construct(IPauseService pauseService) => 
            _pauseService = pauseService;
        
        private void Update()
        {
            if(_pauseService.IsPaused)
                return;
            
            Move();
        }

        private void Move() =>
            transform.position += _direction * _speed * Time.deltaTime;

        public void SetDirection(Vector3 direction) => 
            _direction = direction;

        public void Destroy()
        {
            Destroyed?.Invoke(this);
            Destroy(gameObject);
        }
    
        private void OnCollisionEnter2D(Collision2D other) => Destroy();
    }
}