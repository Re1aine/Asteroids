using UnityEngine;

public interface IInputService
{
    Vector2 Movement { get; }
    bool IsBulletShoot { get; }
    bool IsLaserShoot { get; }
    void Enable();
    void Disable();
}