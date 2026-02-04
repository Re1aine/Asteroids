using System.Collections;
using _Project.Code.Infrastructure.Common.CoroutineService;
using _Project.Code.Logic.Gameplay.Services.Boundries;
using _Project.Code.Logic.Gameplay.Services.Configs;
using _Project.Code.Logic.Gameplay.Services.Configs.Configs.Balance;
using _Project.Code.Logic.Gameplay.Services.Factories.GameFactory;
using _Project.Code.Logic.Gameplay.Services.Holders.UFOsHolder;
using _Project.Code.Logic.Gameplay.Services.Pause;
using _Project.Code.Tools;
using UnityEngine;

namespace _Project.Code.Logic.Gameplay.Services.Spawners.UFOsSpawner
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
        private readonly IConfigsProvider _configsProvider;

        private Coroutine _coroutine;
        
        private UfoSpawnerConfig _config;

        public UFOSpawner(IGameFactory gameFactory, ICoroutineRunner coroutineRunner, IBoundaries boundaries,
            IPauseService pauseService,
            IConfigsProvider configsProvider)
        {
            _gameFactory = gameFactory;
            _coroutineRunner = coroutineRunner;
            _boundaries = boundaries;
            _pauseService = pauseService;
            _configsProvider = configsProvider;

            Configure();
        }

        private void Configure()
        {
            _config = _configsProvider.UfoSpawnerConfig;

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
                yield return new WaitForSecondsUnPaused(_pauseService.IsPaused, _spawnCooldown);
                
                Vector3 randomPos = RandomHelper.GetRandomPointOnCircle(_boundaries.Center, _minRadiusSpawn, _maxRadiusSpawn);
                _gameFactory.CreateUfo(randomPos, Quaternion.identity);
            }
        }
    }
}