using System.Collections;
using Code.Infrastructure.Common.CoroutineService;
using Code.Logic.Gameplay.Services.Boundries;
using Code.Logic.Gameplay.Services.ConfigsProvider;
using Code.Logic.Gameplay.Services.ConfigsProvider.Configs.GameBalance;
using Code.Logic.Gameplay.Services.Factories.GameFactory;
using Code.Logic.Gameplay.Services.Holders.UFOsHolder;
using Code.Logic.Gameplay.Services.PauseService;
using UnityEngine;

namespace Code.Logic.Gameplay.Services.Spawners.UFOsSpawner
{
    public class UFOSpawner : IUFOSpawner
    {
        private float _spawnCooldown = 3f;
        private float _minRadiusSpawn = 10f;
        private float _maxRadiusSpawn = 11f;
    
        private readonly IGameFactory _gameFactory;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IBoundaries _boundaries;
        private readonly IUFOsHolder _ufOsHolder;
        private readonly IPauseService _pauseService;
        private readonly IGameConfigsProvider _gameConfigsProvider;

        private Coroutine _coroutine;
        
        private UfoSpawnerConfig _config;

        public UFOSpawner(IGameFactory gameFactory, ICoroutineRunner coroutineRunner, IBoundaries boundaries,
            IPauseService pauseService,
            IGameConfigsProvider gameConfigsProvider)
        {
            _gameFactory = gameFactory;
            _coroutineRunner = coroutineRunner;
            _boundaries = boundaries;
            _pauseService = pauseService;
            _gameConfigsProvider = gameConfigsProvider;

            Configure();
        }

        private void Configure()
        {
            _config = _gameConfigsProvider.UfoSpawnerConfig;

            _spawnCooldown = _config.SpawnCooldown;
            _minRadiusSpawn = _config.MinRadiusSpawn;
            _maxRadiusSpawn = _config.MaxRadiusSpawn;
        }

        public void Enable() => 
            _coroutine = _coroutineRunner.StartCoroutine(SpawnUfo(), CoroutineScopes.Gameplay);

        public void Disable() => 
            _coroutineRunner.StopCoroutine(_coroutine, CoroutineScopes.Gameplay);

        private IEnumerator SpawnUfo()
        {
            while (true)
            {
                yield return new WaitForSecondsUnPaused(_pauseService, _spawnCooldown);
                
                Vector3 randomPos = RandomHelper.GetRandomPointOnCircle(_boundaries.Center, _minRadiusSpawn, _maxRadiusSpawn);
                _gameFactory.CreateUfo(randomPos, Quaternion.identity);
            }
        }
    }
}