using UnityEngine;

namespace Code.UI.HUD
{
    public abstract class AHUDView : MonoBehaviour
    {
        public abstract void Build();
        
        public void Destroy() =>
            Destroy(gameObject);
    }
}