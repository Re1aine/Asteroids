using R3;

namespace Code.UI.HUD.Menu
{
    public class MenuHUDModel : AHUDModel
    {
        private readonly ReactiveProperty<bool> _isMenuWindowVisible = new();
    
        private readonly CompositeDisposable _disposables = new();
    
        public MenuHUDModel(MenuHUDService hudService)
        {
            _hudService = hudService;
        
            _isMenuWindowVisible
                .Skip(1)
                .Subscribe(isVisible => OnWindowVisibilityChanged(WindowType.MenuWindow, isVisible))
                .AddTo(_disposables);
        }

        public override void ShowWindow(WindowType windowType)
        {
            switch (windowType)
            {
                case WindowType.MenuWindow:
                    _isMenuWindowVisible.Value = true;
                    break;                
            }   
        }

        public override void HideWindow(WindowType windowType)
        {
            switch (windowType)
            {
                case WindowType.MenuWindow:
                    _isMenuWindowVisible.Value = false;
                    break;
            }   
        }

        public override void Dispose()
        {
            _disposables.Dispose();
        }
    }
}