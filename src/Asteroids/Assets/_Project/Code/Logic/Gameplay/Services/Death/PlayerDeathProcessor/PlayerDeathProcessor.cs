using _Project.Code.GameFlow.States.Gameplay;
using _Project.Code.Logic.Gameplay.Entities;
using _Project.Code.Logic.Gameplay.Services.Holders.RepositoriesHolder;
using _Project.Code.Logic.Gameplay.Services.Providers.PlayerProvider;
using _Project.Code.Logic.Gameplay.Services.Revive;
using _Project.Code.Logic.Menu.Services.Purchase.Product;
using _Project.Code.Logic.Services.Repository.Player;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Code.Logic.Gameplay.Services.Death.PlayerDeathProcessor
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