
using Code.Logic.Gameplay.Services.HUDProvider;
using Code.Logic.Gameplay.Services.PlayerDeathObserver;
using Code.Logic.Gameplay.Services.PlayerProvider;
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

        public GameplayStart(GameplayStateMachine gameplayStateMachine, 
            IHUDProvider hudProvider,
            IPlayerProvider playerProvider,
            IPlayerDeathObserver playerDeathObserver, IScoreCountService scoreCountService)
        {
            _gameplayStateMachine = gameplayStateMachine;
            _hudProvider = hudProvider;
            _playerProvider = playerProvider;
            _playerDeathObserver = playerDeathObserver;
            _scoreCountService = scoreCountService;
        }

        public void Enter()
        {
            _hudProvider.Initialize();
            _playerProvider.Initialize();
        
            _playerDeathObserver.Start();
        
            _scoreCountService.Reset();
        
            _gameplayStateMachine.Enter<GameplayLoopState>();
        }

        public void Exit()
        {
        
        }
    }
}