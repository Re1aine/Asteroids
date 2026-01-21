using R3;

namespace Code.UI.HUD.Gameplay
{
    public class GameplayHUDModel : AHUDModel
    {
        private readonly ReactiveProperty<bool> _isLoseWindowVisible = new();
        private readonly ReactiveProperty<bool> _isPlayerStatsWindowVisible = new();
        private readonly ReactiveProperty<bool> _isReviveWindowVisible = new();
        private readonly CompositeDisposable _disposables = new();
     
        public GameplayHUDModel(GameplayHUDService gameplayHUDService)
        {
            _hudService = gameplayHUDService;
         
            _isLoseWindowVisible
                .Skip(1)
                .Subscribe(isVisible => OnWindowVisibilityChanged(WindowType.LoseWindow, isVisible))
                .AddTo(_disposables);
     
            _isPlayerStatsWindowVisible
                .Skip(1)
                .Subscribe(isVisible => OnWindowVisibilityChanged(WindowType.PlayerStatsWindow, isVisible))
                .AddTo(_disposables);
    
            _isReviveWindowVisible
                .Skip(1)
                .Subscribe(isVisible => OnWindowVisibilityChanged(WindowType.ReviveWindow, isVisible))
                .AddTo(_disposables);
         
        }
     
        public override void ShowWindow(WindowType windowType)
        {
            switch (windowType)
            {
                case WindowType.LoseWindow:
                    _isLoseWindowVisible.Value = true;
                    break;
                case WindowType.PlayerStatsWindow:
                    _isPlayerStatsWindowVisible.Value = true;
                    break;
                case WindowType.ReviveWindow:
                    _isReviveWindowVisible.Value = true;
                    break;
             
            }
         
        }
        public override void HideWindow(WindowType windowType)
        {
            switch (windowType)
            {
                case WindowType.LoseWindow:
                    _isLoseWindowVisible.Value = false;
                    break;
                case WindowType.PlayerStatsWindow:
                    _isPlayerStatsWindowVisible.Value = false;
                    break;
                case WindowType.ReviveWindow:
                    _isReviveWindowVisible.Value = false;
                    break;
            }
         
        }
     
        public override void Dispose()
        {
            HideWindow(WindowType.LoseWindow);
            HideWindow(WindowType.PlayerStatsWindow);
    
            _disposables.Dispose();
         
        }
    }
}