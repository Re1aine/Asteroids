using Code.GameFlow.States.Gameplay;
using Code.Logic.Gameplay.Entities;
using Code.Logic.Gameplay.Services.Providers.PlayerProvider;

public class PlayerDeathProcessor : IPlayerDeathProcessor
{
    private readonly GameplayStateMachine _gameplayStateMachine;
    private readonly IReviveService _reviveService;
    private readonly IPlayerProvider _playerProvider;

    private DamageType _damageType;

    public PlayerDeathProcessor(
        GameplayStateMachine gameplayStateMachine,
        IReviveService reviveService,
        IPlayerProvider playerProvider)
    {
        _gameplayStateMachine = gameplayStateMachine;
        _reviveService = reviveService;
        _playerProvider = playerProvider;
    }

    public void StartProcess(DamageType damageType)
    {
        if (_reviveService.IsRevived)
        {
            CompleteProcess();
            return;
        }

        _damageType = damageType;
        _gameplayStateMachine.Enter<ReviveState>();
    }

    public void CancelProcess()
    {
        _reviveService.Revive();
        _gameplayStateMachine.Enter<GameplayLoopState>();
    }

    public void CompleteProcess()
    {
        _playerProvider.Player.Destroy(_damageType);
        _gameplayStateMachine.Enter<LoseState>();
    }
}