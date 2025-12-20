using Code.Logic.Gameplay.Services.Factories.GameFactory;
using Code.Logic.Gameplay.Services.ScoreCounter;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Code.Logic.Gameplay.Entities.Enemy.Asteroid
{
    public class AsteroidDestroyer : IDestroyer
    {
        private readonly AsteroidPresenter _asteroidPresenter;
        private readonly IGameFactory _gameFactory;
        private readonly IScoreCountService _scoreCountService;

        public AsteroidDestroyer(AsteroidPresenter presenter, IGameFactory gameFactory, IScoreCountService scoreCountService)
        {
            _gameFactory = gameFactory;
            _scoreCountService = scoreCountService;
            _asteroidPresenter = presenter;
        }
    
        public void Destroy(DamageType damageType)
        {
            if (_asteroidPresenter.Model.AsteroidType == AsteroidType.Asteroid && damageType == DamageType.Bullet)
                DestroyWithSplit();
            else
                Object.Destroy(_asteroidPresenter.View.gameObject);
            
            _scoreCountService.Add(_asteroidPresenter.Model.ScoreReward);
        }

        private void DestroyWithSplit()
        {
            Object.Destroy(_asteroidPresenter.View.gameObject);
        
            float baseAngle = Random.Range(0f, 360f);
            for (int i = 0; i < 4; i++)
            {
                float angle = baseAngle + i * 90f;
                Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
                CreateSplitAsteroid(direction);
            }
        }
    
        private async void CreateSplitAsteroid(Vector2 direction)
        {
            Vector2 spawnPos = (Vector2)_asteroidPresenter.View.transform.position + direction * 0.5f;
            AsteroidPresenter asteroid = await _gameFactory.CreateAsteroid(spawnPos, RandomHelper.GetRandomRotation(true), AsteroidType.AsteroidPart);
            asteroid.View.SetSpeed(3);
            asteroid.View.SetDirection(direction);
        }
    }
}