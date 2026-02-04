using _Project.Code.Logic.Gameplay.Analytics;
using _Project.Code.Logic.Gameplay.Analytics.AnalyticsStore;
using _Project.Code.Logic.Gameplay.Audio;
using _Project.Code.Logic.Gameplay.Services.Holders.AsteroidsHolder;
using _Project.Code.Logic.Gameplay.Services.Holders.BulletsHolder;
using _Project.Code.Logic.Gameplay.Services.Holders.RepositoriesHolder;
using _Project.Code.Logic.Gameplay.Services.Holders.UFOsHolder;
using _Project.Code.Logic.Gameplay.Services.Holders.VFXHolder;
using _Project.Code.Logic.Gameplay.Services.Revive;
using _Project.Code.Logic.Gameplay.Services.ScoreCounter;
using _Project.Code.Logic.Services.HUDProvider;
using _Project.Code.UI;
using Cysharp.Threading.Tasks;

namespace _Project.Code.GameFlow.States.Gameplay
{
    public class LoseState : IState
    {
        private readonly IHUDProvider _hudProvider;
        private readonly IUFOsHolder _ufosHolder;
        private readonly IAsteroidsHolder _asteroidsHolder;
        private readonly IBulletsHolder _bulletsHolder;
        private readonly IRepositoriesHolder _repositoriesHolder;
        private readonly IScoreCountService _scoreCountService;
        private readonly IAnalytics _analytics;
        private readonly IAnalyticsStore _analyticsStore;
        private readonly IAudioService _audioService;
        private readonly IVFXHolder _vfxHolder;
        private readonly IReviveService _reviveService;
        

        public LoseState(IHUDProvider hudProvider,
            IUFOsHolder ufosHolder,
            IAsteroidsHolder  asteroidsHolder,
            IBulletsHolder bulletsHolder,
            IRepositoriesHolder repositoriesHolder,
            IAnalytics analytics,
            IAnalyticsStore analyticsStore,
            IAudioService audioService,
            IVFXHolder vfxHolder,
            IReviveService reviveService)
        {
            _hudProvider = hudProvider;
            _ufosHolder = ufosHolder;
            _asteroidsHolder = asteroidsHolder;
            _bulletsHolder = bulletsHolder;
            _repositoriesHolder = repositoriesHolder;
            _analytics = analytics;
            _analyticsStore = analyticsStore;
            _audioService = audioService;
            _vfxHolder = vfxHolder;
            _reviveService = reviveService;
        }

        public UniTask Enter()
        {
            _audioService.Reset();            
            
            _repositoriesHolder.SaveAll();

            _hudProvider.HUD.ShowWindow(WindowType.LoseWindow);

            _ufosHolder.DestroyAll();
            _asteroidsHolder.DestroyAll();
            _bulletsHolder.DestroyAll();
            _vfxHolder.DestroyAll();
            
            _analytics.EndSession(_analyticsStore);
            
            _analyticsStore.Flush();
            
            _reviveService.Reset();
            
            return default;
        }

        public UniTask Exit()
        {
            _hudProvider.HUD.Destroy();
            return default;
        }
    }
}