using Code.UI.LoseWindow;
using Code.UI.PlayerStatsWindow;

namespace Code.UI.HUD
{
    public class HUDModel
    {
        public readonly ReadOnlyReactiveProperty<PlayerStatsWindowPresenter> PlayerStatsWindow;
        public readonly ReadOnlyReactiveProperty<LoseWindowPresenter> LoseWindow;

        private readonly ReactiveProperty<PlayerStatsWindowPresenter> _playerStatsWindow;
        private readonly ReactiveProperty<LoseWindowPresenter> _loseWindow;
    
        public HUDModel()
        {
            _playerStatsWindow = new ReactiveProperty<PlayerStatsWindowPresenter>(null);
            _loseWindow = new ReactiveProperty<LoseWindowPresenter>(null);
        
            PlayerStatsWindow = new ReadOnlyReactiveProperty<PlayerStatsWindowPresenter>(_playerStatsWindow);
            LoseWindow = new ReadOnlyReactiveProperty<LoseWindowPresenter>(_loseWindow);
        }

        public void SetPlayerStatsWindow(PlayerStatsWindowPresenter window) => _playerStatsWindow.SetValue(window);
        public void SetLoseWindow(LoseWindowPresenter window) => _loseWindow.SetValue(window);
    }
}