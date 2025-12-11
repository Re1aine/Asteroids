using System.Threading.Tasks;
using Code.Logic.Gameplay.Entities.Enemy.Asteroid;
using Code.Logic.Gameplay.Entities.Enemy.UFO;
using Code.Logic.Gameplay.Entities.Player;
using Code.Logic.Gameplay.Projectiles.Bullet;
using Code.Logic.Gameplay.Projectiles.LaserBeam;
using Code.UI.HUD;
using Code.UI.LoseWindow;
using Code.UI.PlayerStatsWindow;
using UnityEngine;

namespace Code.Logic.Gameplay.Services.Factories.GameFactory
{
    public interface IGameFactory
    {
        Task<PlayerPresenter> CreatePlayer(Vector3 position, Quaternion rotation);
        Task<AsteroidPresenter> CreateAsteroid(Vector3 position, Quaternion rotation, AsteroidType asteroidType, int scoreReward = 2);
        Task<UFOPresenter> CreateUfo(Vector3 position, Quaternion rotation, int scoreReward = 4);
        Task<Bullet> CreateBullet(Vector3 position, Quaternion rotation);
        Task<LaserBeam> CreateLaserBeam(Vector2 position, Quaternion rotation);
        Task<HUDPresenter> CreateHUD();
        Task<LoseWindowPresenter> CreateLoseWindow();
        Task<PlayerStatsWindowPresenter> CreatePlayerStatsWindow();
    }
}