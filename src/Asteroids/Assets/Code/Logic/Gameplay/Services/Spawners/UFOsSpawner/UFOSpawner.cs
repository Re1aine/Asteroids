using System.Collections;
using Code.Infrastructure.Common.CoroutineService;
using Code.Logic.Gameplay.Services.Boundries;
using Code.Logic.Gameplay.Services.Factories.GameFactory;
using Code.Logic.Gameplay.Services.Holders.UFOsHolder;
using UnityEngine;

namespace Code.Logic.Gameplay.Services.Spawners.UFOsSpawner
{
    public class UFOSpawner : IUFOSpawner
    {
        private const float SpawnCooldown = 3f;
        private const float MinRadiusSpawn = 10f;
        private const float MaxRadiusSpawn = 11f;
    
        private readonly IGameFactory _gameFactory;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IBoundaries _boundaries;
        private readonly IUFOsHolder _ufOsHolder;
        private readonly IPauseService _pauseService;

        private Coroutine _coroutine;
    
        public UFOSpawner(IGameFactory gameFactory, ICoroutineRunner coroutineRunner, IBoundaries boundaries, IPauseService pauseService)
        {
            _gameFactory = gameFactory;
            _coroutineRunner = coroutineRunner;
            _boundaries = boundaries;
            _pauseService = pauseService;
        }

        public void Enable() => 
            _coroutine = _coroutineRunner.StartCoroutine(SpawnUfo(), CoroutineScopes.Gameplay);

        public void Disable() => 
            _coroutineRunner.StopCoroutine(_coroutine, CoroutineScopes.Gameplay);

        private IEnumerator SpawnUfo()
        {
            while (true)
            {
                yield return new WaitForSecondsUnPaused(_pauseService, SpawnCooldown);
                
                Vector3 randomPos = RandomHelper.GetRandomPointOnCircle(_boundaries.Center, MinRadiusSpawn, MaxRadiusSpawn);
                _gameFactory.CreateUfo(randomPos, Quaternion.identity);
            }
        }
    }
}