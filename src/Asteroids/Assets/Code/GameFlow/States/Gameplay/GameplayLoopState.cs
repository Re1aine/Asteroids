using UnityEngine;

public class GameplayLoopState : IState
{
    private readonly GameplayStateMachine _gameplayStateMachine;
    private readonly IInputService _inputService;
    private readonly IAsteroidSpawner _asteroidSpawner;
    private readonly IUFOSpawner _ufoSpawner;
    private readonly IGameFactory _gameFactory;
    private readonly IPlayerDeathObserver _playerDeathObserver;
    private readonly IHUDProvider _hudProvider;

    private PlayerStatsWindow _playerStatsWindow;

    public GameplayLoopState(GameplayStateMachine gameplayStateMachine,IInputService inputService,
        IAsteroidSpawner asteroidSpawner,
        IUFOSpawner ufoSpawner,
        IGameFactory gameFactory, IHUDProvider hudProvider, IPlayerDeathObserver playerDeathObserver)
    {
        _gameplayStateMachine = gameplayStateMachine;
        _inputService = inputService;
        _asteroidSpawner = asteroidSpawner;
        _ufoSpawner = ufoSpawner;
        _gameFactory = gameFactory;
        _playerDeathObserver = playerDeathObserver;
        _hudProvider = hudProvider;
    }

    public void Enter()
    {
        _inputService.Enable();
        _asteroidSpawner.Enable();
        _ufoSpawner.Enable();

        _playerStatsWindow = _gameFactory.CreatePlayerStatsWindow(_hudProvider.HUD.transform);
    }

    public void Exit()
    {
        _inputService.Disable();
        _asteroidSpawner.Disable();
        _ufoSpawner.Disable();
        
        _playerDeathObserver.StopObserveDeath();
        
        _playerStatsWindow.Destroy();
    }
}