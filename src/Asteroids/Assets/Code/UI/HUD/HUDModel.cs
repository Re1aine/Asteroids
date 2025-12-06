using System;
using R3;

namespace Code.UI.HUD
{
    public class HUDModel : IDisposable
    {
        public ReadOnlyReactiveProperty<bool> IsLoseWindowVisible  => _isLoseWindowVisible;
        public ReadOnlyReactiveProperty<bool> IsPlayerStatsWindowVisible  => _isPlayerStatsWindowVisible;

        private readonly ReactiveProperty<bool> _isLoseWindowVisible = new();
        private readonly ReactiveProperty<bool> _isPlayerStatsWindowVisible = new();
        
        public void ShowLoseWindow() => 
            _isLoseWindowVisible.Value = true;

        public void ShowPlayerStatsWindow() => 
            _isPlayerStatsWindowVisible.Value = true;

        public void HidePlayerStatsWindow() => 
            _isPlayerStatsWindowVisible.Value = false;

        public void HideLoseWindow() => 
            _isLoseWindowVisible.Value = false;

        public void Dispose()
        {
            HideLoseWindow();
            HidePlayerStatsWindow();
        }
    }
}