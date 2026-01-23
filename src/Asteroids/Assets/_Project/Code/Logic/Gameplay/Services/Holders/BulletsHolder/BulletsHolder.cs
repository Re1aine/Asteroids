using System.Collections.Generic;
using System.Linq;
using _Project.Code.Logic.Gameplay.Projectiles.Bullet;
using UnityEngine;

namespace _Project.Code.Logic.Gameplay.Services.Holders.BulletsHolder
{
    public class BulletsHolder : IBulletsHolder
    {
        public IReadOnlyList<Bullet> Bullets => _bullets;
    
        private readonly List<Bullet> _bullets = new();
    
        public void Add(Bullet bullet)
        {
            _bullets.Add(bullet);
            bullet.Destroyed += OnDestroyed;
        }
    
        public void Remove(Bullet bullet)
        {
            _bullets.Remove(bullet);
            bullet.Destroyed -= OnDestroyed;
        }

        public void DestroyAll()
        {
            _bullets.ToList().ForEach(x => Object.Destroy(x.gameObject));
            _bullets.Clear();
        }
        private void OnDestroyed(Bullet bullet) => Remove(bullet);
    }
}