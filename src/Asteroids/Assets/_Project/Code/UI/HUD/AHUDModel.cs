using System;
using R3;

namespace _Project.Code.UI.HUD
{
    public abstract class AHUDModel : IDisposable
    {
        public Observable<Unit> Builded => _builded;
        private readonly Subject<Unit> _builded = new();
        
        protected AHUDService _hudService;
    
        public abstract void ShowWindow(WindowType windowType);

        public abstract void HideWindow(WindowType windowType);

        public void Build() => 
            _builded.OnNext(Unit.Default);

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