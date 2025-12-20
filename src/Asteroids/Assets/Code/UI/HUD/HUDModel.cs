using System;
using R3;

namespace Code.UI.HUD
{
    public class HUDModel : IDisposable
    {
        private readonly HUDService _hudService;
        
        private readonly ReactiveProperty<bool> _isLoseWindowVisible = new();
        private readonly ReactiveProperty<bool> _isPlayerStatsWindowVisible = new();
        private readonly ReactiveProperty<bool> _isRewardWindowVisible = new();
        
        private readonly CompositeDisposable _disposables = new();
        
        public HUDModel(HUDService hudService)
        {
            _hudService = hudService;
            
            _isLoseWindowVisible
                .Skip(1)
                .Subscribe(OnLoseWindowVisibilityChanged)
                .AddTo(_disposables);
             
            _isPlayerStatsWindowVisible
                .Skip(1)
                .Subscribe(OnPlayerStatsWindowVisibilityChanged)
                .AddTo(_disposables);
            
            _isRewardWindowVisible
                .Skip(1)
                .Subscribe(OnRewardWindowVisibilityChanged)
                .AddTo(_disposables);
        }
        
        private void OnLoseWindowVisibilityChanged(bool isVisible)
        {
            if (isVisible)
                _hudService.ShowLoseWindow();
            else
                _hudService.HideLoseWindow();
        }
        
        private void OnPlayerStatsWindowVisibilityChanged(bool isVisible)
        {
            if (isVisible)
                _hudService.ShowPlayerStatsWindow();
            else
                _hudService.HidePlayerStatsWindow();
        }
        private void OnRewardWindowVisibilityChanged(bool isVisible)
        {
            if (isVisible)
                _hudService.ShowRewardWindow();
            else
                _hudService.HideRewardWindow();
        }
        
        public void ShowLoseWindow() => 
            _isLoseWindowVisible.Value = true;
        
        public void ShowPlayerStatsWindow() => 
            _isPlayerStatsWindowVisible.Value = true;
        
        public void ShowRewardWindow() => 
            _isRewardWindowVisible.Value = true;

        public void HideLoseWindow() => 
            _isLoseWindowVisible.Value = false;

        public void HidePlayerStatsWindow() => 
            _isPlayerStatsWindowVisible.Value = false;

        public void HideRewardWindow() => 
            _isRewardWindowVisible.Value = false;

        public void Dispose()
        {
            HideLoseWindow();
            HidePlayerStatsWindow();
            
            _disposables.Dispose();
        }
    }
}