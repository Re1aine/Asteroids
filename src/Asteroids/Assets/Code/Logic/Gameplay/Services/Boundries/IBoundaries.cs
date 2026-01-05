using UnityEngine;

namespace Code.Logic.Gameplay.Services.Boundries
{
    public interface IBoundaries
    {
        void Initialize();
        Vector2 Min { get; }
        Vector2 Max { get; }
        Vector2 Center { get; }
    }
}