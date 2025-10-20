using Code.GameFlow.States.Gameplay;
using Code.Logic.Gameplay.Services.Providers.PlayerProvider;

namespace Code.Logic.Gameplay.Services.Observers.PlayerDeathObserver
{
    public class PlayerDeathObserver : IPlayerDeathObserver
    {
        private readonly GameplayStateMachine _gameplayStateMachine;
        private readonly IPlayerProvider _playerProvider;

        public PlayerDeathObserver(GameplayStateMachine gameplayStateMachine, IPlayerProvider playerProvider)
        {
            _gameplayStateMachine = gameplayStateMachine;
            _playerProvider = playerProvider;
        }

        public void Start() => _playerProvider.Player.Destroyed += OnDeath;
        public void Stop() => _playerProvider.Player.Destroyed -= OnDeath;
        private void OnDeath() => _gameplayStateMachine.Enter<LoseState>();
    }
}