using System.Collections.Generic;
using System.Linq;
using Code.Logic.Gameplay.Asteroid;
using UnityEngine;

namespace Code.Logic.Gameplay.Services.AsteroidsHolder
{
    public class AsteroidsHolder : IAsteroidsHolder
    {
        public IReadOnlyList<AsteroidPresenter> Asteroids => _asteroids;
    
        private readonly List<AsteroidPresenter> _asteroids = new();
    
        public void Add(AsteroidPresenter asteroid)
        {
            _asteroids.Add(asteroid);
            asteroid.Destroyed += OnDestroyed;
        }

        public void Remove(AsteroidPresenter asteroid)
        {
            _asteroids.Remove(asteroid);
            asteroid.Destroyed -= OnDestroyed;
        }

        public void DestroyAll()
        {
            _asteroids.ToList().ForEach(x => Object.Destroy(x.View.gameObject));
            _asteroids.Clear();
        }

        private void OnDestroyed(AsteroidPresenter asteroid) => Remove(asteroid);
    }
}