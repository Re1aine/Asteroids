using System;
using _Project.Code.Logic.Gameplay.Entities;
using _Project.Code.Logic.Gameplay.Entities.Player;
using _Project.Code.Logic.Gameplay.Services.AdService;
using _Project.Code.Logic.Gameplay.Services.AdService.Ad;
using _Project.Code.Logic.Gameplay.Services.Death.PlayerDeathProcessor;
using _Project.Code.Logic.Gameplay.Services.Providers.PlayerProvider;
using VContainer.Unity;

namespace _Project.Code.Logic.Gameplay.Services.Death.PlayerDeathService
{
    public class PlayerDeathService : IPlayerDeathService, IInitializable, IDisposable
    {
        private readonly IAdsService _adsService;
        private readonly IPlayerProvider _playerProvider;
        private readonly IPlayerDeathProcessor _playerDeathProcessor;

        private PlayerPresenter _presenter;
    
        public PlayerDeathService(IAdsService adsService, IPlayerProvider playerProvider, IPlayerDeathProcessor playerDeathProcessor)
        {
            _adsService = adsService;
            _playerProvider = playerProvider;
            _playerDeathProcessor = playerDeathProcessor;
        
            _playerProvider.PlayerChanged += OnPlayerChanged;
            _adsService.AdCompleted += OnAdCompleted;
        }

        public void Initialize() => 
            _presenter.DamageReceiver.LethalDamageReceived += OnReceivedLethalDamage;

        private void OnPlayerChanged(PlayerPresenter presenter) => 
            _presenter = presenter;

        private void OnAdCompleted(AdContext context)
        {
            if (context == AdContext.DeathRevive)
                _playerDeathProcessor.CancelProcess();
            else
                _playerDeathProcessor.CompleteProcess();
        }

        private void OnReceivedLethalDamage(DamageType damageType) => 
            _playerDeathProcessor.StartProcess(damageType);

        public void Dispose()
        {
            _playerProvider.PlayerChanged -= OnPlayerChanged;
            _adsService.AdCompleted -= OnAdCompleted;
            _presenter.DamageReceiver.LethalDamageReceived -= OnReceivedLethalDamage;
        }
    }
}