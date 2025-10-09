using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code.Logic.Gameplay.Services.BulletsHolder
{
    public class BulletsHolder : IBulletsHolder
    {
        public IReadOnlyList<Bullet.Bullet> Bullets => _bullets;
    
        private readonly List<Bullet.Bullet> _bullets = new();
    
        public void Add(Bullet.Bullet bullet)
        {
            _bullets.Add(bullet);
            bullet.Destroyed += OnDestroyed;
        }
    
        public void Remove(Bullet.Bullet bullet)
        {
            _bullets.Remove(bullet);
            bullet.Destroyed -= OnDestroyed;
        }

        public void DestroyAll()
        {
            _bullets.ToList().ForEach(x => Object.Destroy(x.gameObject));
            _bullets.Clear();
        }
        private void OnDestroyed(Bullet.Bullet bullet) => Remove(bullet);
    }
}