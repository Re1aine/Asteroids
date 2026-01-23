using UnityEngine;

namespace _Project.Code.UI.HUD
{
    public abstract class AHUDView : MonoBehaviour
    {
        public abstract void Build();
        
        public void Destroy() =>
            Destroy(gameObject);
    }
}