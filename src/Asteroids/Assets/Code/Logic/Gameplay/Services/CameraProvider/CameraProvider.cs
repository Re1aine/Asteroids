using UnityEngine;

namespace Code.Logic.Gameplay.Services.CameraProvider
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