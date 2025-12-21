using Code.GameFlow.States.Gameplay;
using Code.Logic.Gameplay.Entities;
using Code.Logic.Gameplay.Services.Providers.PlayerProvider;
using Code.Logic.Gameplay.Services.ReviveService;
using Cysharp.Threading.Tasks;

namespace Code.Logic.Gameplay.Services.DeathProcessor
{
    public class PlayerDeathProcessor : IPlayerDeathProcessor
    {
        private readonly GameplayStateMachine _gameplayStateMachine;
        private readonly IReviveService _reviveService;
        private readonly IPlayerProvider _playerProvider;
        private readonly ShockWaveEffector _shockWaveEffector;

        private DamageType _damageType;

        public PlayerDeathProcessor(
            GameplayStateMachine gameplayStateMachine,
            IReviveService reviveService,
            IPlayerProvider playerProvider,
            ShockWaveEffector shockWaveEffector)
        {
            _gameplayStateMachine = gameplayStateMachine;
            _reviveService = reviveService;
            _playerProvider = playerProvider;
            _shockWaveEffector = shockWaveEffector;
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
            _shockWaveEffector.CreateShockWave().Forget();
            _gameplayStateMachine.Enter<GameplayLoopState>();
        }

        public void CompleteProcess()
        {
            _playerProvider.Player.Destroy(_damageType);
            _gameplayStateMachine.Enter<LoseState>();
        }
    }
}