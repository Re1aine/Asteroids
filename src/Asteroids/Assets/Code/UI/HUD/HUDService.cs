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
                    _loseWindow = _gameFactory.CreateLoseWindow();
                else 
                    _loseWindow?.Destroy();
            })
            .AddTo(_disposables);

        model.IsPlayerStatsWindowVisible
            .Subscribe(isVisible =>
            {
                if (isVisible)
                    _playerStatsWindow = _gameFactory.CreatePlayerStatsWindow();
                else
                    _playerStatsWindow?.Destroy();
            })
            .AddTo(_disposables);
    }

    public void ShowLoseWindow() => 
        _loseWindow = _gameFactory.CreateLoseWindow();

    public void ShowPlayerStatsWindow() => 
        _playerStatsWindow = _gameFactory.CreatePlayerStatsWindow();

    public void HideLoseWindow() => 
        _loseWindow.Destroy();

    public void HidePlayerStatsWindow() => 
        _playerStatsWindow.Destroy();

    public void Dispose() =>
        _disposables.Dispose();
}