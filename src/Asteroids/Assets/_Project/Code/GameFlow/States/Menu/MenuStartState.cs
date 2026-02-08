using _Project.Code.Logic.Gameplay.Audio;
using _Project.Code.Logic.Gameplay.Services.Holders.RepositoriesHolder;
using _Project.Code.Logic.Services.HUDProvider;
using _Project.Code.UI;
using Cysharp.Threading.Tasks;

namespace _Project.Code.GameFlow.States.Menu
{
    public class MenuStartState : IState
    {
        private readonly IHUDProvider _hudProvider;
        private readonly IRepositoriesHolder _repositoriesHolder;
        private readonly IAudioService _audioService;

        public MenuStartState(IHUDProvider hudProvider, IRepositoriesHolder repositoriesHolder, IAudioService audioService)
        {
            _hudProvider = hudProvider;
            _repositoriesHolder = repositoriesHolder;
            _audioService = audioService;
        }

        public UniTask Enter()
        {
            _repositoriesHolder.LoadAll();
        
            _hudProvider.HUD.Build();
        
            _hudProvider.HUD.ShowWindow(WindowType.MenuWindow);
        
            _audioService.PlaySound(SoundType.MainMenuMusic);

            return default;
        }

        public UniTask Exit()
        {
            _audioService.StopSound(SoundType.MainMenuMusic);
            
            return default;
        }
    }
}