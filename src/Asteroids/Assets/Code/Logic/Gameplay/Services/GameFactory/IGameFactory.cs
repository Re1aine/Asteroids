using Code.Logic.Gameplay.Asteroid;
using Code.Logic.Gameplay.Player;
using Code.Logic.Gameplay.UFO;
using Code.UI;
using Code.UI.HUD;
using Code.UI.LoseWindow;
using Code.UI.PlayerStatsWindow;
using UnityEngine;

namespace Code.Logic.Gameplay.Services.GameFactory
{
    public interface IGameFactory
    {
        PlayerPresenter CreatePlayer(Vector3 position, Quaternion rotation);
        AsteroidPresenter CreateAsteroid(Vector3 position, Quaternion rotation, AsteroidType asteroidType, int scoreReward = 2);
        UFOPresenter CreateUfo(Vector3 position, Quaternion rotation, int scoreReward = 4);
        Bullet.Bullet CreateBullet(Vector3 position, Quaternion rotation);
        LaserBeam.LaserBeam CreateLaserBeam(Vector2 position, Quaternion rotation);
        HUDPresenter CreateHUD();
        LoseWindowPresenter CreateLoseWindow(Transform parent);
        PlayerStatsWindowPresenter CreatePlayerStatsWindow(Transform parent);
    }
}