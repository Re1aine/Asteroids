using UnityEngine;

public interface IPointWrapService
{
    Vector2 WrapPoint(Vector2 point, Vector2 correction);
}