using _Project.Code.Logic.Gameplay.Audio;
using _Project.Code.Logic.Gameplay.Services.Death.PlayerDeathService;
using _Project.Code.Logic.Gameplay.Services.Input;
using _Project.Code.Logic.Gameplay.Services.Observers.Player.PlayerGunObserver;
using _Project.Code.Logic.Gameplay.Services.ScoreCounter;
using _Project.Code.Logic.Gameplay.Services.Spawners.AsteroidsSpawner;
using _Project.Code.Logic.Gameplay.Services.Spawners.UFOsSpawner;
using _Project.Code.Logic.Services.HUDProvider;
using _Project.Code.UI;
using _Project.Code.UI.PlayerStatsWindow;
using Cysharp.Threading.Tasks;

namespace _Project.Code.GameFlow.States.Gameplay
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

        public UniTask Enter()
        {
            _scoreCountService.Reset();
            
            _playerGunObserver.Start();
            
            _inputService.Enable();
            _asteroidSpawner.Enable();
            _ufoSpawner.Enable();
            
            _audioService.PlaySound(SoundType.Music);
            
            _hudProvider.HUD.ShowWindow(WindowType.PlayerStatsWindow);
            _hudProvider.HUD.ShowWindow(WindowType.TipWindow);
                
            return default;
        }

        public UniTask Exit()
        {
            _inputService.Disable();
            _asteroidSpawner.Disable();
            _ufoSpawner.Disable();

            _playerGunObserver.Stop();
            
            _hudProvider.HUD.HideWindow(WindowType.PlayerStatsWindow);
            _hudProvider.HUD.HideWindow(WindowType.TipWindow);
                
            return default;
        }
    }
}