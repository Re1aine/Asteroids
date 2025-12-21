using Code.Logic.Gameplay.Audio;
using Code.Logic.Gameplay.Services.Input;
using Code.Logic.Gameplay.Services.PauseService;
using Code.Logic.Gameplay.Services.Providers.HUDProvider;

namespace Code.GameFlow.States.Gameplay
{
    public class ReviveState : IState
    {
        private readonly IHUDProvider _hudProvider;
        private readonly IPauseService _pauseService;
        private readonly IInputService _inputService;
        private readonly IAudioService _audioService;

        public ReviveState(
            IHUDProvider hudProvider,
            IPauseService pauseService,
            IInputService inputService,
            IAudioService audioService)
        {
            _hudProvider = hudProvider;
            _pauseService = pauseService;
            _inputService = inputService;
            _audioService = audioService;
        }

        public void Enter()
        {
            _inputService.Disable();
        
            _pauseService.Pause();
        
            _audioService.StopSoundCategory(SoundCategory.ShortSounds);
            _audioService.PauseSoundCategory(SoundCategory.Music);
        
            _hudProvider.HUD.ShowRewardWindow();
        }

        public void Exit()
        {
            _pauseService.UnPause();
            _hudProvider.HUD.HideRewardWindow();
        }
    }
}