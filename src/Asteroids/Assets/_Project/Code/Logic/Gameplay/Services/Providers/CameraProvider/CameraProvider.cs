using UnityEngine;

namespace _Project.Code.Logic.Gameplay.Services.Providers.CameraProvider
{
    public class CameraProvider : ICameraProvider
    {
        public Camera Camera { get; }

        public CameraProvider(Camera camera)
        {
            Camera = camera;
        }
    }
}