using Code.Logic.Gameplay.Services.Factories.GameFactory;
using Code.UI.LoseWindow;
using Code.UI.PlayerStatsWindow;

namespace Code.UI.HUD
{
    public class GameplayHUDService : AHUDService
    {
        private readonly IGameplayUIFactory _gameplayUIFactory;
    
        private LoseWindowPresenter _loseWindow;
        private PlayerStatsWindowPresenter _playerStatsWindow;
        private ReviveWindowPresenter _reviveWindow;
    
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
            }    
        }
    }
}