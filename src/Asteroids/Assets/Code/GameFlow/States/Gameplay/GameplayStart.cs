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
        private readonly IScoreCountService _scoreCountService;
        private readonly IRepositoriesHolder _repositoriesHolder;

        public GameplayStart(GameplayStateMachine gameplayStateMachine, 
            IHUDProvider hudProvider,
            IPlayerProvider playerProvider,
            IPlayerDeathObserver playerDeathObserver,
            IScoreCountService scoreCountService,
            IRepositoriesHolder repositoriesHolder)
        {
            _gameplayStateMachine = gameplayStateMachine;
            _hudProvider = hudProvider;
            _playerProvider = playerProvider;
            _playerDeathObserver = playerDeathObserver;
            _scoreCountService = scoreCountService;
            _repositoriesHolder = repositoriesHolder;
        }
        
        public void Enter()
        {
            _repositoriesHolder.LoadAll();
            
            _playerProvider.Initialize();
            _hudProvider.Initialize();
            
            _playerDeathObserver.Start();
        
            _scoreCountService.Reset();
        
            _gameplayStateMachine.Enter<GameplayLoopState>();
        }

        public void Exit()
        {
        
        }
    }
}