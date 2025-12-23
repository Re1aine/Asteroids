using Code.Infrastructure.Common.AssetsManagement.AssetLoader;
using Code.Logic.Gameplay.Analytics;
using Code.Logic.Gameplay.Audio;
using Code.Logic.Gameplay.Services.AdService;
using Code.Logic.Gameplay.Services.ConfigsProvider;
using Code.Logic.Gameplay.Services.Factories.GameFactory;
using Code.Logic.Gameplay.Services.Holders.RepositoriesHolder;
using Code.Logic.Gameplay.Services.PlayerDeathService;
using Code.Logic.Gameplay.Services.Providers.HUDProvider;
using Code.Logic.Gameplay.Services.Providers.PlayerProvider;

namespace Code.GameFlow.States.Gameplay
{
    public class GameplayStart : IState
    {
        private readonly GameplayStateMachine _gameplayStateMachine;
        private readonly IHUDProvider _hudProvider;
        private readonly IPlayerProvider _playerProvider;
        private readonly IRepositoriesHolder _repositoriesHolder;
        private readonly IAnalytics _analytics;
        private readonly IAddressablesAssetsLoader _addressablesAssetsLoader;
        private readonly IGameFactory _gameFactory;
        private readonly IAudioService _audioService;
        private readonly IConfigsProvider _configsProvider;
        private readonly IAdsService _adsService;
        private readonly IPlayerDeathService _playerDeathService;
        private readonly ISDKInitializer _sdkInitializer;

        public GameplayStart(GameplayStateMachine gameplayStateMachine, 
            IHUDProvider hudProvider,
            IPlayerProvider playerProvider,
            IRepositoriesHolder repositoriesHolder,
            IAnalytics analytics,
            IAddressablesAssetsLoader addressablesAssetsLoader,
            IGameFactory gameFactory,
            IAudioService audioService,
            IConfigsProvider configsProvider,
            IAdsService adsService,
            IPlayerDeathService playerDeathService, ISDKInitializer sdkInitializer)
        {
            _gameplayStateMachine = gameplayStateMachine;
            _hudProvider = hudProvider;
            _playerProvider = playerProvider;
            _repositoriesHolder = repositoriesHolder;
            _analytics = analytics;
            _addressablesAssetsLoader = addressablesAssetsLoader;
            _gameFactory = gameFactory;
            _audioService = audioService;
            _configsProvider = configsProvider;
            _adsService = adsService;
            _playerDeathService = playerDeathService;
            _sdkInitializer = sdkInitializer;
        }
        
        public async void Enter()
        {
            await _addressablesAssetsLoader.Initialize();
            await _sdkInitializer.Initialize();
            
            _analytics.Initialize();
            
            await _configsProvider.Initialize();
            
            _repositoriesHolder.LoadAll();

            _gameFactory.WarmUp();
            
            _adsService.Initialize();
            
            await _playerProvider.Initialize();
            await _hudProvider.Initialize();

            _audioService.Initialize();
            
            _playerDeathService.Initialize();
            
            _analytics.StartSession();

            _gameplayStateMachine.Enter<GameplayLoopState>();
        }

        public void Exit()
        {
        
        }
    }
}