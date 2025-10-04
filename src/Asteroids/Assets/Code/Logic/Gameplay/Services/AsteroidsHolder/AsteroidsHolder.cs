using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AsteroidsHolder : IAsteroidsHolder
{
    public IReadOnlyList<AsteroidPresenter> Asteroids => _asteroids;
    
    private readonly List<AsteroidPresenter> _asteroids = new();
    
    public void Add(AsteroidPresenter asteroid) => _asteroids.Add(asteroid);
    public void Remove(AsteroidPresenter asteroid) => _asteroids.Remove(asteroid);
    public void DestroyAll()
    {
        _asteroids.ToList().ForEach(x => Object.Destroy(x.View.gameObject));
        _asteroids.Clear();
    }
}