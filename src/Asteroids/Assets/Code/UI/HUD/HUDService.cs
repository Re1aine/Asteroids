using Code.Logic.Gameplay.Services.Factories.GameFactory;
using Code.UI.LoseWindow;
using Code.UI.PlayerStatsWindow;

namespace Code.UI.HUD
{
    public class HUDService
    {
        private readonly IGameFactory _gameFactory;
    
        private LoseWindowPresenter _loseWindow;
        private PlayerStatsWindowPresenter _playerStatsWindow;
        private ReviveWindowPresenter _reviveWindow;
    
        public HUDService(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        public async void ShowLoseWindow() => 
            _loseWindow = await _gameFactory.CreateLoseWindow();

        public async void ShowPlayerStatsWindow() => 
            _playerStatsWindow = await _gameFactory.CreatePlayerStatsWindow();

        public async void ShowRewardWindow() => 
            _reviveWindow = await _gameFactory.CreateRevivedWindow();

        public void HideLoseWindow() => 
            _loseWindow.Destroy();

        public void HidePlayerStatsWindow() =>
            _playerStatsWindow.Destroy();

        public void HideRewardWindow() => 
            _reviveWindow.Destroy();
    }
}