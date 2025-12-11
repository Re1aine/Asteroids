using System.Threading.Tasks;
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

    public async Task ShowLoseWindow() => 
        _loseWindow = await _gameFactory.CreateLoseWindow();

    public async Task ShowPlayerStatsWindow() => 
        _playerStatsWindow = await _gameFactory.CreatePlayerStatsWindow();

    public void HideLoseWindow() => 
        _loseWindow.Destroy();

    public void HidePlayerStatsWindow() =>
        _playerStatsWindow.Destroy();
    
}