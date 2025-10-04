using System.Collections;
using UnityEngine;

public class UFOSpawner : IUFOSpawner
{
    private const float SpawnCooldown = 8f;
    private const float MinRadiusSpawn = 15f;
    private const float MaxRadiusSpawn = 20f;
    
    private readonly IGameFactory _gameFactory;
    private readonly ICoroutineRunner _coroutineRunner;
    private readonly IBoundaries _boundaries;
    private readonly IUFOsHolder _ufOsHolder;

    private Coroutine _coroutine;
    
    public UFOSpawner(IGameFactory gameFactory, ICoroutineRunner coroutineRunner, IBoundaries boundaries)
    {
        _gameFactory = gameFactory;
        _coroutineRunner = coroutineRunner;
        _boundaries = boundaries;
    }

    public void Enable() => 
        _coroutine = _coroutineRunner.StartCoroutine(SpawnUfo(), CoroutineScopes.Gameplay);

    public void Disable() => 
        _coroutineRunner.StopCoroutine(_coroutine, CoroutineScopes.Gameplay);

    private IEnumerator SpawnUfo()
    {
        while (true)
        {
            Vector3 randomPos = RandomHelper.GetRandomPointOnCircle(_boundaries.Center, MinRadiusSpawn, MaxRadiusSpawn);
            _gameFactory.CreateUfo(randomPos, Quaternion.identity);
            
            yield return new WaitForSeconds(SpawnCooldown);
        }
    }
}