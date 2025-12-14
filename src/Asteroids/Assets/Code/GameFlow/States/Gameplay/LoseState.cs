using Code.Logic.Gameplay.Analytics;
using Code.Logic.Gameplay.Analytics.AnalyticsStore;
using Code.Logic.Gameplay.Services.Holders.AsteroidsHolder;
using Code.Logic.Gameplay.Services.Holders.BulletsHolder;
using Code.Logic.Gameplay.Services.Holders.RepositoriesHolder;
using Code.Logic.Gameplay.Services.Holders.UFOsHolder;
using Code.Logic.Gameplay.Services.Providers.HUDProvider;
using Code.Logic.Gameplay.Services.ScoreCounter;
using Code.UI.LoseWindow;

namespace Code.GameFlow.States.Gameplay
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

        private LoseWindowPresenter _loseWindow;

        public LoseState(IHUDProvider hudProvider,
            IUFOsHolder ufosHolder,
            IAsteroidsHolder  asteroidsHolder,
            IBulletsHolder bulletsHolder,
            IRepositoriesHolder repositoriesHolder,
            IAnalytics analytics,
            IAnalyticsStore analyticsStore,
            IAudioService audioService,
            IVFXHolder vfxHolder)
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
        }

        public void Enter()
        {
            _audioService.StopAllSounds();
            
            _repositoriesHolder.SaveAll();

            _hudProvider.HUD.ShowLoseWindow();

            _ufosHolder.DestroyAll();
            _asteroidsHolder.DestroyAll();
            _bulletsHolder.DestroyAll();
            _vfxHolder.DestroyAll();
            
            _analytics.EndSession(_analyticsStore);
            
            _analyticsStore.Flush();
        }

        public void Exit()
        {
            _hudProvider.HUD.Destroy();
        }
    }
}