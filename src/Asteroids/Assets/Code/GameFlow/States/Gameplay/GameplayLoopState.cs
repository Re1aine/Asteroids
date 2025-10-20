using Code.Logic.Gameplay.Services.Input;
using Code.Logic.Gameplay.Services.Observers.PlayerDeathObserver;
using Code.Logic.Gameplay.Services.Providers.HUDProvider;
using Code.Logic.Gameplay.Services.Spawners.AsteroidsSpawner;
using Code.Logic.Gameplay.Services.Spawners.UFOsSpawner;
using Code.UI.PlayerStatsWindow;

namespace Code.GameFlow.States.Gameplay
{
    public class GameplayLoopState : IState
    {
        private readonly IInputService _inputService;
        private readonly IAsteroidSpawner _asteroidSpawner;
        private readonly IUFOSpawner _ufoSpawner;
        private readonly IHUDProvider _hudProvider;
        private readonly IPlayerDeathObserver _playerDeathObserver;
        private readonly IPlayerGunObserver _playerGunObserver;

        private PlayerStatsWindowPresenter _playerStatsWindow;

        public GameplayLoopState(IInputService inputService,
            IAsteroidSpawner asteroidSpawner,
            IUFOSpawner ufoSpawner, 
            IHUDProvider hudProvider,
            IPlayerDeathObserver playerDeathObserver,
            IPlayerGunObserver playerGunObserver)
        {
            _inputService = inputService;
            _asteroidSpawner = asteroidSpawner;
            _ufoSpawner = ufoSpawner;
            _hudProvider = hudProvider;
            _playerDeathObserver = playerDeathObserver;
            _playerGunObserver = playerGunObserver;
        }

        public void Enter()
        {
            _inputService.Enable();
            _asteroidSpawner.Enable();
            _ufoSpawner.Enable();
            
            _hudProvider.HUD.CreatePlayerStatsWindow();
        }

        public void Exit()
        {
            _inputService.Disable();
            _asteroidSpawner.Disable();
            _ufoSpawner.Disable();
        
            _playerDeathObserver.Stop();
            _playerGunObserver.Stop();
        
            _hudProvider.HUD.DestroyPlayerStatsWindow();
        }
    }
}