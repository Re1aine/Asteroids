using Code.Logic.Gameplay.Services.Holders.RepositoriesHolder;
using Code.Logic.Gameplay.Services.Observers.PlayerDeathObserver;
using Code.Logic.Gameplay.Services.Providers.HUDProvider;
using Code.Logic.Gameplay.Services.Providers.PlayerProvider;
using Code.Logic.Gameplay.Services.ScoreCounter;

namespace Code.GameFlow.States.Gameplay
{
    public class GameplayStart : IState
    {
        private readonly GameplayStateMachine _gameplayStateMachine;
        private readonly IHUDProvider _hudProvider;
        private readonly IPlayerProvider _playerProvider;
        private readonly IPlayerDeathObserver _playerDeathObserver;
        private readonly IPlayerGunObserver _playerGunObserver;
        private readonly IScoreCountService _scoreCountService;
        private readonly IRepositoriesHolder _repositoriesHolder;
        private readonly IAnalytics _analytics;

        public GameplayStart(GameplayStateMachine gameplayStateMachine, 
            IHUDProvider hudProvider,
            IPlayerProvider playerProvider,
            IPlayerDeathObserver playerDeathObserver,
            IPlayerGunObserver playerGunObserver,
            IScoreCountService scoreCountService,
            IRepositoriesHolder repositoriesHolder,
            IAnalytics analytics)
        {
            _gameplayStateMachine = gameplayStateMachine;
            _hudProvider = hudProvider;
            _playerProvider = playerProvider;
            _playerDeathObserver = playerDeathObserver;
            _playerGunObserver = playerGunObserver;
            _scoreCountService = scoreCountService;
            _repositoriesHolder = repositoriesHolder;
            _analytics = analytics;
        }
        
        public async void Enter()
        {
            await _analytics.InitializeAsync();
            
            _repositoriesHolder.LoadAll();
            
            _playerProvider.Initialize();
            _hudProvider.Initialize();
            
            _playerDeathObserver.Start();
            _playerGunObserver.Start();
        
            _scoreCountService.Reset();
        
            _analytics.StartSession();
            
            _gameplayStateMachine.Enter<GameplayLoopState>();
        }

        public void Exit()
        {
        
        }
    }
}