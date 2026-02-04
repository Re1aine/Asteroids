using System.Collections.Generic;
using _Project.Code.Logic.Gameplay.Entities.Enemy.Asteroid;

namespace _Project.Code.Logic.Gameplay.Services.Holders.AsteroidsHolder
{
    public interface IAsteroidsHolder
    {
        IReadOnlyList<AsteroidPresenter> Asteroids { get; }
        void Add(AsteroidPresenter asteroid);
        void Remove(AsteroidPresenter asteroid);
        void DestroyAll();
    }
}