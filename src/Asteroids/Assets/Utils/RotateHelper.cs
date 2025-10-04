using UnityEngine;

public static class RotateHelper
{
    public static Quaternion GetRotation2D(Vector3 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        return Quaternion.Euler(0, 0, angle);
    }
}