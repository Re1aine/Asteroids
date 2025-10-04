using System.Collections;
using UnityEngine;

public class AsteroidSpawner : IAsteroidSpawner
{
    private const float SpawnCooldown = 7f;
    private const float MinRadiusSpawn = 15f;
    private const float MaxRadiusSpawn = 20f;
    
    private readonly IGameFactory _gameFactory;
    private readonly ICoroutineRunner _coroutineRunner;
    private readonly IBoundaries _boundaries;

    private Coroutine _coroutine;
    
    public AsteroidSpawner(IGameFactory gameFactory, ICoroutineRunner coroutineRunner, IBoundaries boundaries)
    {
        _gameFactory = gameFactory;
        _coroutineRunner = coroutineRunner;
        _boundaries = boundaries;
    }

    public void Enable() => 
        _coroutine = _coroutineRunner.StartCoroutine(SpawnAsteroids(), CoroutineScopes.Gameplay);

    public void Disable() => 
        _coroutineRunner.StopCoroutine(_coroutine, CoroutineScopes.Gameplay);

    private IEnumerator SpawnAsteroids()
    {
        while (true)
        {
            Vector3 randomPos = RandomHelper.GetRandomPointOnCircle(_boundaries.Center, MinRadiusSpawn, MaxRadiusSpawn);
            
            AsteroidPresenter asteroid = _gameFactory.CreateAsteroid(randomPos, Quaternion.identity, AsteroidType.Asteroid);
            asteroid.View.LaunchInRandomDirection();
            
            yield return new WaitForSeconds(SpawnCooldown);
        }
    }
}