using Code.Logic.Gameplay.Services.Providers.CameraProvider;
using UnityEngine;
using VContainer;

namespace Code.Logic.Gameplay.Services.Boundries
{
    public sealed class ScreenBoundaries : IBoundaries
    {
        public Vector2 Min { get; private set; }
        public Vector2 Max { get; private set; }
        public Vector2 Center { get; private set; }

        [Inject]
        public ScreenBoundaries(ICameraProvider cameraProvider)
        {
            float halfHeight = cameraProvider.Camera.orthographicSize;
            float halfWidth = halfHeight * cameraProvider.Camera.aspect;
            Vector2 pos = cameraProvider.Camera.transform.position;
            Min = pos - new Vector2(halfWidth, halfHeight);
            Max = pos + new Vector2(halfWidth, halfHeight);
            Center = pos;
            
        }
    }
}