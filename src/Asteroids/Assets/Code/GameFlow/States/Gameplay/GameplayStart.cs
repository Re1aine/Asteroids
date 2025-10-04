using UnityEngine;

public class GameplayStart : IState
{
    private readonly GameplayStateMachine _gameplayStateMachine;
    private readonly IHUDProvider _hudProvider;
    private readonly IPlayerProvider _playerProvider;
    private readonly IPlayerDeathObserver _playerDeathObserver;

    public GameplayStart(GameplayStateMachine gameplayStateMachine, 
        IHUDProvider hudProvider,
        IPlayerProvider playerProvider,
        IPlayerDeathObserver playerDeathObserver)
    {
        _gameplayStateMachine = gameplayStateMachine;
        _hudProvider = hudProvider;
        _playerProvider = playerProvider;
        _playerDeathObserver = playerDeathObserver;
    }

    public void Enter()
    {
        _hudProvider.Initialize();
        _playerProvider.Initialize();
        
        _playerDeathObserver.StartObserveDeath();
        
        _gameplayStateMachine.Enter<GameplayLoopState>();
    }

    public void Exit()
    {
        
    }
}