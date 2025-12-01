using System;
using Code.Logic.Gameplay.Services.Factories.GameFactory;
using Code.UI.HUD;
using Code.UI.LoseWindow;
using Code.UI.PlayerStatsWindow;
using R3;

public class HUDService : IDisposable
{
    private readonly IGameFactory _gameFactory;

    private LoseWindowPresenter _loseWindow;
    private PlayerStatsWindowPresenter _playerStatsWindow;
    
    private readonly CompositeDisposable _disposables = new();

    public HUDService(IGameFactory gameFactory, HUDModel model)
    {
        _gameFactory = gameFactory;
        
        model.IsLoseWindowVisible
            .Subscribe(isVisible =>
            {
                if (isVisible) 
                    ShowLoseWindow();
                else 
                    HideLoseWindow();
            })
            .AddTo(_disposables);

        model.IsPlayerStatsWindowVisible
            .Subscribe(isVisible =>
            {
                if (isVisible)
                    ShowPlayerStatsWindow();
                else
                    HidePlayerStatsWindow();
            })
            .AddTo(_disposables);
    }

    private void ShowLoseWindow() => 
        _loseWindow = _gameFactory.CreateLoseWindow();
    
    private void ShowPlayerStatsWindow() => 
        _playerStatsWindow = _gameFactory.CreatePlayerStatsWindow();
    
    private void HideLoseWindow() => 
        _loseWindow?.Destroy();
    
    private void HidePlayerStatsWindow() => 
        _playerStatsWindow?.Destroy();

    public void Dispose() =>
        _disposables.Dispose();
}