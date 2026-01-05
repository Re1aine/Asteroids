using UnityEngine;

public abstract class AHUDView : MonoBehaviour
{
    public void Destroy() =>
        Destroy(gameObject);
}