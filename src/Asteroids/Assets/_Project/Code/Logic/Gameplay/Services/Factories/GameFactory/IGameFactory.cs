using _Project.Code.Logic.Gameplay.Entities.Enemy.Asteroid;
using _Project.Code.Logic.Gameplay.Entities.Enemy.UFO;
using _Project.Code.Logic.Gameplay.Entities.Player;
using _Project.Code.Logic.Gameplay.Projectiles.Bullet;
using _Project.Code.Logic.Gameplay.Projectiles.LaserBeam;
using _Project.Code.Logic.Gameplay.Services.Configs.Configs.Assets;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Code.Logic.Gameplay.Services.Factories.GameFactory
{
    public interface IGameFactory
    {
        UniTask<PlayerPresenter> CreatePlayer(Vector3 position, Quaternion rotation);
        UniTask<AsteroidPresenter> CreateAsteroid(Vector3 position, Quaternion rotation, AsteroidType asteroidType, int scoreReward = 2);
        UniTask<UFOPresenter> CreateUfo(Vector3 position, Quaternion rotation, int scoreReward = 4);
        UniTask<Bullet> CreateBullet(Vector3 position, Quaternion rotation);
        UniTask<LaserBeam> CreateLaserBeam(Vector2 position, Quaternion rotation);
        VFX CreateVFX(VFXType type, Vector3 position, Quaternion rotation);
    }
}