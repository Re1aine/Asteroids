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
        PlayerPresenter CreatePlayer(Vector3 position, Quaternion rotation);
        AsteroidPresenter CreateAsteroid(Vector3 position, Quaternion rotation, AsteroidType asteroidType, int scoreReward = 2);
        UFOPresenter CreateUfo(Vector3 position, Quaternion rotation, int scoreReward = 4);
        Bullet CreateBullet(Vector3 position, Quaternion rotation);
        LaserBeam CreateLaserBeam(Vector2 position, Quaternion rotation);
        HUDPresenter CreateHUD();
        LoseWindowPresenter CreateLoseWindow();
        PlayerStatsWindowPresenter CreatePlayerStatsWindow();
        AudioPlayer CreateAudioPlayer();
    }
}