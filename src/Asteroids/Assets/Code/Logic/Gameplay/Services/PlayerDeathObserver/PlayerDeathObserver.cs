public class PlayerDeathObserver : IPlayerDeathObserver
{
    private readonly GameplayStateMachine _gameplayStateMachine;
    private readonly IPlayerProvider _playerProvider;

    public PlayerDeathObserver(GameplayStateMachine gameplayStateMachine, IPlayerProvider playerProvider)
    {
        _gameplayStateMachine = gameplayStateMachine;
        _playerProvider = playerProvider;
    }

    public void StartObserveDeath() => _playerProvider.Player.Destroyed += OnPlayerDeath;
    public void StopObserveDeath() => _playerProvider.Player.Destroyed -= OnPlayerDeath;
    private void OnPlayerDeath() => _gameplayStateMachine.Enter<LoseState>();
}