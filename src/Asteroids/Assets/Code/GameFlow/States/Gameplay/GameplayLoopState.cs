using Code.Logic.Gameplay.Analytics;
using Code.Logic.Gameplay.Services.Input;
using Code.Logic.Gameplay.Services.Providers.HUDProvider;
using Code.Logic.Gameplay.Services.ScoreCounter;
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
        private readonly IPlayerGunObserver _playerGunObserver;
        private readonly IAudioService _audioService;
        private readonly IScoreCountService _scoreCountService;
        private readonly IPlayerDeathService _playerDeathService;

        private PlayerStatsWindowPresenter _playerStatsWindow;

        public GameplayLoopState(IInputService inputService,
            IAsteroidSpawner asteroidSpawner,
            IUFOSpawner ufoSpawner, 
            IHUDProvider hudProvider,
            IPlayerGunObserver playerGunObserver,
            IAudioService audioService,
            IScoreCountService scoreCountService)
        {
            _inputService = inputService;
            _asteroidSpawner = asteroidSpawner;
            _ufoSpawner = ufoSpawner;
            _hudProvider = hudProvider;
            _playerGunObserver = playerGunObserver;
            _audioService = audioService;
            _scoreCountService = scoreCountService;
        }

        public void Enter()
        {
            _scoreCountService.Reset();
            
            _playerGunObserver.Start();
            
            _inputService.Enable();
            //_asteroidSpawner.Enable();
            //_ufoSpawner.Enable();
            
            _audioService.PlaySound(SoundType.Music);
            
            _hudProvider.HUD.ShowPlayerStatsWindow();
        }

        public void Exit()
        {
            _inputService.Disable();
            _asteroidSpawner.Disable();
            _ufoSpawner.Disable();

            _playerGunObserver.Stop();
            
            _hudProvider.HUD.HidePlayerStatsWindow();
        }
    }
}