using UnityEngine;

namespace Code.UI.HUD
{
    public abstract class AHUDView : MonoBehaviour
    {
        public void Destroy() =>
            Destroy(gameObject);
    }
}