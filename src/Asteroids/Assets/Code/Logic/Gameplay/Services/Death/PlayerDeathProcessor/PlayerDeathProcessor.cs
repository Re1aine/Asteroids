using Code.GameFlow.States.Gameplay;
using Code.Logic.Gameplay.Entities;
using Code.Logic.Gameplay.Services.Holders.RepositoriesHolder;
using Code.Logic.Gameplay.Services.Providers.PlayerProvider;
using Code.Logic.Gameplay.Services.Repository.Player;
using Code.Logic.Gameplay.Services.ReviveService;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Logic.Gameplay.Services.DeathProcessor
{
    public class PlayerDeathProcessor : IPlayerDeathProcessor
    {
        private readonly GameplayStateMachine _gameplayStateMachine;
        private readonly IReviveService _reviveService;
        private readonly IPlayerProvider _playerProvider;
        private readonly ShockWaveEffector _shockWaveEffector;
        
        private readonly PlayerRepository _playerRepository;

        private DamageType _damageType;

        public PlayerDeathProcessor(
            GameplayStateMachine gameplayStateMachine,
            IReviveService reviveService,
            IPlayerProvider playerProvider,
            ShockWaveEffector shockWaveEffector,
            IRepositoriesHolder repositoriesHolder)
        {
            _gameplayStateMachine = gameplayStateMachine;
            _reviveService = reviveService;
            _playerProvider = playerProvider;
            _shockWaveEffector = shockWaveEffector;
            _playerRepository = repositoriesHolder.GetRepository<PlayerRepository>();
        }

        public void StartProcess(DamageType damageType)
        {
            if (_reviveService.IsRevived)
            {
                CompleteProcess();
                return;
            }

            if (_playerRepository.IsAdsRemoved.CurrentValue)
            {
                Debug.Log($"<color=yellow><b>Player has [{nameof(ProductId.AdsRemoval)}] purchase. Proceed instantly revive.");
                CancelProcess();
                return;
            }

            _damageType = damageType;
            _gameplayStateMachine.Enter<ReviveState>().Forget();
        }

        public void CancelProcess()
        {
            _reviveService.Revive();
            _shockWaveEffector.CreateShockWave().Forget();
            _gameplayStateMachine.Enter<GameplayLoopState>().Forget();
        }

        public void CompleteProcess()
        {
            _playerProvider.Player.Destroy(_damageType);
            _gameplayStateMachine.Enter<LoseState>().Forget();
        }
    }
}