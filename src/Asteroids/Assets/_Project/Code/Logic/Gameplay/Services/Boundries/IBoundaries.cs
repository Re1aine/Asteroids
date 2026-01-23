using UnityEngine;

namespace _Project.Code.Logic.Gameplay.Services.Boundries
{
    public interface IBoundaries
    {
        Vector2 Min { get; }
        Vector2 Max { get; }
        Vector2 Center { get; }
    }
}