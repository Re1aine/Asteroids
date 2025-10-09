
using Code.Logic.Gameplay.Services.AsteroidsSpawner;
using Code.Logic.Gameplay.Services.GameFactory;
using Code.Logic.Gameplay.Services.HUDProvider;
using Code.Logic.Gameplay.Services.Input;
using Code.Logic.Gameplay.Services.PlayerDeathObserver;
using Code.Logic.Gameplay.Services.UFOsSpawner;
using Code.UI;

namespace Code.GameFlow.States.Gameplay
{
    public class GameplayLoopState : IState
    {
        private readonly IInputService _inputService;
        private readonly IAsteroidSpawner _asteroidSpawner;
        private readonly IUFOSpawner _ufoSpawner;
        private readonly IGameFactory _gameFactory;
        private readonly IPlayerDeathObserver _playerDeathObserver;
        private readonly IHUDProvider _hudProvider;

        private PlayerStatsWindow _playerStatsWindow;

        public GameplayLoopState(IInputService inputService,
            IAsteroidSpawner asteroidSpawner,
            IUFOSpawner ufoSpawner,
            IGameFactory gameFactory, IHUDProvider hudProvider,
            IPlayerDeathObserver playerDeathObserver)
        {
            _inputService = inputService;
            _asteroidSpawner = asteroidSpawner;
            _ufoSpawner = ufoSpawner;
            _gameFactory = gameFactory;
            _playerDeathObserver = playerDeathObserver;
            _hudProvider = hudProvider;
        }

        public void Enter()
        {
            _inputService.Enable();
            _asteroidSpawner.Enable();
            _ufoSpawner.Enable();

            _playerStatsWindow = _gameFactory.CreatePlayerStatsWindow(_hudProvider.HUD.transform);
        }

        public void Exit()
        {
            _inputService.Disable();
            _asteroidSpawner.Disable();
            _ufoSpawner.Disable();
        
            _playerDeathObserver.Stop();
        
            _playerStatsWindow.Destroy();
        }
    }
}