using Code.Logic.Gameplay.Services.Providers.CameraProvider;
using UnityEngine;
using VContainer.Unity;

namespace Code.Logic.Gameplay.Services.Boundries
{
    public sealed class ScreenBoundaries : IBoundaries, IInitializable
    {
        private readonly ICameraProvider _cameraProvider;
        public Vector2 Min { get; private set; }
        public Vector2 Max { get; private set; }
        public Vector2 Center { get; private set; }

        public ScreenBoundaries(ICameraProvider cameraProvider)
        {
            _cameraProvider = cameraProvider;
        }

        public void Initialize()
        {
            float halfHeight = _cameraProvider.Camera.orthographicSize;
            float halfWidth = halfHeight * _cameraProvider.Camera.aspect;
            Vector2 pos = _cameraProvider.Camera.transform.position;
            
            Min = pos - new Vector2(halfWidth, halfHeight);
            Max = pos + new Vector2(halfWidth, halfHeight);
            Center = pos;
        }
    }
}