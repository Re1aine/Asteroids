using _Project.Code.UI.LoseWindow;
using _Project.Code.UI.PlayerStatsWindow;
using _Project.Code.UI.ReviveWindow;
using _Project.Code.UI.UIFactory.GameplayUIFactory;

namespace _Project.Code.UI.HUD.Gameplay
{
    public class GameplayHUDService : AHUDService
    {
        private readonly IGameplayUIFactory _gameplayUIFactory;
    
        private LoseWindowPresenter _loseWindow;
        private PlayerStatsWindowPresenter _playerStatsWindow;
        private ReviveWindowPresenter _reviveWindow;
        
        private TipView _tipWindow;
    
        public GameplayHUDService(IGameplayUIFactory gameplayUIFactory)
        {
            _gameplayUIFactory = gameplayUIFactory;
        }

        public override async void ShowWindow(WindowType windowType)
        {
            switch (windowType)
            {
                case WindowType.LoseWindow:
                    _loseWindow = await _gameplayUIFactory.CreateLoseWindow();
                    break;
                case WindowType.PlayerStatsWindow:
                    _playerStatsWindow = await _gameplayUIFactory.CreatePlayerStatsWindow();
                    break;
                case WindowType.ReviveWindow:
                    _reviveWindow = await _gameplayUIFactory.CreateReviveWindow();
                    break;
                case WindowType.TipWindow:
                    _tipWindow = await _gameplayUIFactory.CreateTipWindow();
                    break;
            }
        }

        public override void HideWindow(WindowType windowType)
        {
            switch (windowType)
            {
                case WindowType.LoseWindow:
                    _loseWindow.Destroy();
                    break;
                case WindowType.PlayerStatsWindow:
                    _playerStatsWindow.Destroy();
                    break;
                case WindowType.ReviveWindow:
                    _reviveWindow.Destroy();
                    break;
                case WindowType.TipWindow:
                    _tipWindow.Destroy().Forget();
                    break;
            }    
        }
    }
}