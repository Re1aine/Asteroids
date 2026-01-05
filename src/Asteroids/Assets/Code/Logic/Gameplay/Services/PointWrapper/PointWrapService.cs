using Code.Logic.Gameplay.Services.Boundries;
using UnityEngine;

namespace Code.Logic.Gameplay.Services.PointWrapper
{
    public class PointWrapService : IPointWrapService
    {
        private readonly IBoundaries _boundaries;
        
        public PointWrapService(IBoundaries boundaries)
        {
            _boundaries = boundaries;
        }
    
        public Vector2 WrapPoint(Vector2 point) => WrapPoint(point, Vector2.zero);

        public Vector2 WrapPoint(Vector2 point, Vector2 correction) => new(
            WrapCoordinate(point.x, _boundaries.Min.x - correction.x, _boundaries.Max.x + correction.x),
            WrapCoordinate(point.y, _boundaries.Min.y - correction.y, _boundaries.Max.y + correction.y));

        private static float WrapCoordinate(float value, float min, float max) =>
            Mathf.Repeat(value - min, max - min) + min;
    }
}