using _Project.Code.Logic.Gameplay.Analytics;
using _Project.Code.Logic.Gameplay.Services.AdService;
using _Project.Code.Logic.Gameplay.Services.Configs.BalanceConfigsProvider;
using _Project.Code.Logic.Gameplay.Services.Death.PlayerDeathService;
using _Project.Code.Logic.Gameplay.Services.Providers.PlayerProvider;
using _Project.Code.Logic.Services.HUDProvider;
using Cysharp.Threading.Tasks;

namespace _Project.Code.GameFlow.States.Gameplay
{
    public class GameplayInitState : IState
    {
        private readonly GameplayStateMachine _gameplayStateMachine;
        private readonly IHUDProvider _hudProvider;
        private readonly IPlayerProvider _playerProvider;
        private readonly IAnalytics _analytics;
        private readonly IAdsService _adsService;
        private readonly IPlayerDeathService _playerDeathService;
        private readonly IBalanceConfigsProvider _balanceConfigsProvider;
        
        public GameplayInitState(GameplayStateMachine gameplayStateMachine, 
            IHUDProvider hudProvider,
            IPlayerProvider playerProvider,
            IAnalytics analytics,
            IAdsService adsService,
            IPlayerDeathService playerDeathService,
            IBalanceConfigsProvider balanceConfigsProvider)
        {
            _gameplayStateMachine = gameplayStateMachine;
            _hudProvider = hudProvider;
            _playerProvider = playerProvider;
            _analytics = analytics;
            _adsService = adsService;
            _playerDeathService = playerDeathService;
            _balanceConfigsProvider = balanceConfigsProvider;
        }

        public async UniTask Enter()
        {
            await _balanceConfigsProvider.Initialize();
            
            _analytics.Initialize();
            
            _adsService.Initialize();
            
            await _playerProvider.Initialize();
            await _hudProvider.Initialize();
            
            _playerDeathService.Initialize();
            
            _analytics.StartSession();

            _gameplayStateMachine.Enter<GameplayLoopState>().Forget();
        }

        public UniTask Exit() => default;
    }
}