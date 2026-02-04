using UnityEngine;

namespace _Project.Code.Logic.Gameplay.Services.Input
{
    public interface IInputService
    {
        Vector2 Movement { get; }
        bool IsBulletShoot { get; }
        bool IsLaserShoot { get; }
        void Enable();
        void Disable();
    }
}