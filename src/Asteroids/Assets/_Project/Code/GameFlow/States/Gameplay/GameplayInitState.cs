using Code.Logic.Gameplay;
using Code.Logic.Gameplay.Analytics;
using Code.Logic.Gameplay.Audio;
using Code.Logic.Gameplay.Services.AdService;
using Code.Logic.Gameplay.Services.Boundries;
using Code.Logic.Gameplay.Services.Configs;
using Code.Logic.Gameplay.Services.Death.PlayerDeathService;
using Code.Logic.Gameplay.Services.Factories.GameFactory;
using Code.Logic.Gameplay.Services.Providers.PlayerProvider;
using Code.Logic.Services.HUDProvider;
using Cysharp.Threading.Tasks;

namespace Code.GameFlow.States.Gameplay
{
    public class GameplayInitState : IState
    {
        private readonly GameplayStateMachine _gameplayStateMachine;
        private readonly IHUDProvider _hudProvider;
        private readonly IPlayerProvider _playerProvider;
        private readonly IAnalytics _analytics;
        private readonly IGameFactory _gameFactory;
        private readonly IAdsService _adsService;
        private readonly IPlayerDeathService _playerDeathService;
        private readonly IConfigsProvider _configsProvider;
        private readonly IAudioService _audioService;

        public GameplayInitState(GameplayStateMachine gameplayStateMachine, 
            IHUDProvider hudProvider,
            IPlayerProvider playerProvider,
            IAnalytics analytics,
            IGameFactory gameFactory,
            IAdsService adsService,
            IPlayerDeathService playerDeathService,
            IConfigsProvider configsProvider,
            IAudioService audioService)
        {
            _gameplayStateMachine = gameplayStateMachine;
            _hudProvider = hudProvider;
            _playerProvider = playerProvider;
            _analytics = analytics;
            _gameFactory = gameFactory;
            _adsService = adsService;
            _playerDeathService = playerDeathService;
            _configsProvider = configsProvider;
            _audioService = audioService;
        }

        public async UniTask Enter()
        {
            await _configsProvider.Initialize();
            
            _analytics.Initialize();
            
            await _gameFactory.WarmUp();
            
            _adsService.Initialize();
            
            await _playerProvider.Initialize();
            await _hudProvider.Initialize();

            _audioService.Initialize();
            
            _playerDeathService.Initialize();
            
            _analytics.StartSession();

            _gameplayStateMachine.Enter<GameplayLoopState>().Forget();
        }

        public UniTask Exit() => default;
    }
}