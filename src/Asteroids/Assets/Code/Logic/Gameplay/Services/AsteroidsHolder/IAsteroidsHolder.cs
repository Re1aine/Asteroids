using System.Collections.Generic;

public interface IAsteroidsHolder
{
    IReadOnlyList<AsteroidPresenter> Asteroids { get; }
    void Add(AsteroidPresenter asteroid);
    void Remove(AsteroidPresenter asteroid);
    void DestroyAll();
}