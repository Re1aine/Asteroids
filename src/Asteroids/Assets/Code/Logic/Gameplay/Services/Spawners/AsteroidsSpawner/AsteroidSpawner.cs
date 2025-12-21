using System.Collections;
using Code.Infrastructure.Common.CoroutineService;
using Code.Logic.Gameplay.Entities.Enemy.Asteroid;
using Code.Logic.Gameplay.Services.Boundries;
using Code.Logic.Gameplay.Services.Factories.GameFactory;
using Code.Logic.Gameplay.Services.PauseService;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Logic.Gameplay.Services.Spawners.AsteroidsSpawner
{
    public class AsteroidSpawner : IAsteroidSpawner
    {
        private const float SpawnCooldown = 5f;
        private const float MinRadiusSpawn = 10f;
        private const float MaxRadiusSpawn = 11f;
    
        private readonly IGameFactory _gameFactory;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IBoundaries _boundaries;
        private readonly IPauseService _pauseService;

        private Coroutine _coroutine;
    
        public AsteroidSpawner(IGameFactory gameFactory, ICoroutineRunner coroutineRunner, IBoundaries boundaries, IPauseService pauseService)
        {
            _gameFactory = gameFactory;
            _coroutineRunner = coroutineRunner;
            _boundaries = boundaries;
            _pauseService = pauseService;
        }

        public void Enable() => 
            _coroutine = _coroutineRunner.StartCoroutine(SpawnAsteroids(), CoroutineScopes.Gameplay);
        
        public void Disable() => 
            _coroutineRunner.StopCoroutine(_coroutine, CoroutineScopes.Gameplay);
        
        private IEnumerator SpawnAsteroids()
        {
            while (true)
            {
                yield return new WaitForSecondsUnPaused(_pauseService, SpawnCooldown);
                
                Vector3 randomPos = RandomHelper.GetRandomPointOnCircle(_boundaries.Center, MinRadiusSpawn, MaxRadiusSpawn);
            
                UniTask<AsteroidPresenter> asteroidTask = _gameFactory.CreateAsteroid(randomPos, Quaternion.identity, AsteroidType.Asteroid);

                yield return UniTask.WaitUntil(() => asteroidTask.Status == UniTaskStatus.Succeeded).ToCoroutine();
                
                var asteroid = asteroidTask
                    .GetAwaiter()
                    .GetResult();
                
                asteroid.View.SetRandomDirection();            
            }
        }
    }
}