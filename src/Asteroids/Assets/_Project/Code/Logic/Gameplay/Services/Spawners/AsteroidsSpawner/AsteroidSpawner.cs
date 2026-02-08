using System.Collections;
using _Project.Code.Infrastructure.Common.CoroutineService;
using _Project.Code.Logic.Gameplay.Entities.Enemy.Asteroid;
using _Project.Code.Logic.Gameplay.Services.Boundries;
using _Project.Code.Logic.Gameplay.Services.Configs.BalanceConfigsProvider;
using _Project.Code.Logic.Gameplay.Services.Configs.Configs.Balance;
using _Project.Code.Logic.Gameplay.Services.Factories.GameFactory;
using _Project.Code.Logic.Gameplay.Services.Pause;
using _Project.Code.Tools;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Code.Logic.Gameplay.Services.Spawners.AsteroidsSpawner
{
    public class AsteroidSpawner : IAsteroidSpawner
    {
        private float _spawnCooldown = 5f;
        private float _minRadiusSpawn = 10f;
        private float _maxRadiusSpawn = 11f;
    
        private readonly IGameFactory _gameFactory;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IBoundaries _boundaries;
        private readonly IPauseService _pauseService;
        private readonly IBalanceConfigsProvider _balanceConfigsProvider;

        private Coroutine _coroutine;
        
        private AsteroidSpawnerConfig _config;
    
        public AsteroidSpawner(IGameFactory gameFactory, ICoroutineRunner coroutineRunner, IBoundaries boundaries,
            IPauseService pauseService,
            IBalanceConfigsProvider balanceConfigsProvider)
        {
            _gameFactory = gameFactory;
            _coroutineRunner = coroutineRunner;
            _boundaries = boundaries;
            _pauseService = pauseService;
            _balanceConfigsProvider = balanceConfigsProvider;
            
            Configure();
        }

        private void Configure()
        {
            _config = _balanceConfigsProvider.AsteroidSpawnerConfig;

            _spawnCooldown = _config.SpawnCooldown;
            _minRadiusSpawn = _config.MinRadiusSpawn;
            _maxRadiusSpawn = _config.MaxRadiusSpawn;
        }
        
        public void Enable() => 
            _coroutine = _coroutineRunner.StartCoroutine(SpawnAsteroids(), CoroutineScopes.Gameplay);
        
        public void Disable() => 
            _coroutineRunner.StopCoroutine(_coroutine, CoroutineScopes.Gameplay);
        
        private IEnumerator SpawnAsteroids()
        {
            while (true)
            {
                yield return new WaitForSecondsUnPaused(_pauseService.IsPaused, _spawnCooldown);
                
                Vector3 randomPos = RandomHelper.GetRandomPointOnCircle(_boundaries.Center, _minRadiusSpawn, _maxRadiusSpawn);
            
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