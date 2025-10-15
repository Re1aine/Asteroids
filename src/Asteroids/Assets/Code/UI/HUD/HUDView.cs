using UnityEngine;

namespace Code.UI.HUD
{
    public class HUDView : MonoBehaviour
    {
        public void Destroy() =>
            Destroy(gameObject);
    }
}