using System;
using UnityEngine;

namespace Code.Logic.Gameplay.Bullet
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CapsuleCollider2D))]
    public class Bullet : MonoBehaviour
    { 
        public event Action<Bullet> Destroyed;
    
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private float _speed;
    
        public void MoveToDirection(Vector2 direction) => 
            _rigidbody2D.AddForce(direction * _speed, ForceMode2D.Impulse);

        public void Destroy()
        {
            Destroyed?.Invoke(this);
            Destroy(gameObject);
        }
    
        private void OnCollisionEnter2D(Collision2D other) => Destroy();
    }
}