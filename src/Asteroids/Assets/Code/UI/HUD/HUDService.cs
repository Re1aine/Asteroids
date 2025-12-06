using Code.Logic.Gameplay.Services.Factories.GameFactory;
using Code.UI.LoseWindow;
using Code.UI.PlayerStatsWindow;

public class HUDService
{
    private readonly IGameFactory _gameFactory;
    
    private LoseWindowPresenter _loseWindow;
    private PlayerStatsWindowPresenter _playerStatsWindow;

    public HUDService(IGameFactory gameFactory)
    {
        _gameFactory = gameFactory;
    }

    public void ShowLoseWindow() => 
        _loseWindow = _gameFactory.CreateLoseWindow();

    public void ShowPlayerStatsWindow() => 
        _playerStatsWindow = _gameFactory.CreatePlayerStatsWindow();

    public void HideLoseWindow() => 
        _loseWindow.Destroy();

    public void HidePlayerStatsWindow() =>
        _playerStatsWindow.Destroy();
    
}