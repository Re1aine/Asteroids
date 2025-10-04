using UnityEngine;

public interface IGameFactory
{
    PlayerPresenter CreatePlayer(Vector3 position, Quaternion rotation);
    AsteroidPresenter CreateAsteroid(Vector3 position, Quaternion rotation, AsteroidType asteroidType, int scoreReward = 2);
    UFOPresenter CreateUfo(Vector3 position, Quaternion rotation, int scoreReward = 4);
    Bullet CreateBullet(Vector3 position, Quaternion rotation);
    LaserBeam CreateLaserBeam(Vector2 position, Quaternion rotation);
    HUD CreateHUD();
    LoseWindow CreateLoseWindow(Transform parent);
    PlayerStatsWindow CreatePlayerStatsWindow(Transform parent);
}