using System;

namespace Code.UI.HUD
{
    public abstract class AHUDModel : IDisposable
    {
        protected AHUDService _hudService;
    
        public abstract void ShowWindow(WindowType windowType);

        public abstract void HideWindow(WindowType windowType);
    
        protected void OnWindowVisibilityChanged(WindowType windowType, bool isVisible)
        {
            if (isVisible)
                _hudService.ShowWindow(windowType);
            else
                _hudService.HideWindow(windowType);
         
        }
    
        public abstract void Dispose();
    }
}