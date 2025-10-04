using UnityEngine;

public interface IBoundaries
{
    Vector2 Min { get; }
    Vector2 Max { get; }
    Vector2 Center { get; }
}