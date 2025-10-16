using Code.Logic.Gameplay.Services.Factories.GameFactory;
using Code.Logic.Gameplay.Services.Holders.AsteroidsHolder;
using Code.Logic.Gameplay.Services.Holders.BulletsHolder;
using Code.Logic.Gameplay.Services.Holders.UFOsHolder;
using Code.Logic.Gameplay.Services.Providers.HUDProvider;
using Code.Logic.Gameplay.Services.ScoreCounter;
using Code.UI;
using Code.UI.LoseWindow;

namespace Code.GameFlow.States.Gameplay
{
    public class LoseState : IState
    {
        private readonly IHUDProvider _hudProvider;
        private readonly IUFOsHolder _ufosHolder;
        private readonly IAsteroidsHolder _asteroidsHolder;
        private readonly IBulletsHolder _bulletsHolder;
        private readonly IScoreCountService _scoreCountService;

        private LoseWindowPresenter _loseWindow;

        public LoseState(IHUDProvider hudProvider,
            IUFOsHolder ufosHolder,
            IAsteroidsHolder  asteroidsHolder,
            IBulletsHolder bulletsHolder)
        {
            _hudProvider = hudProvider;
            _ufosHolder = ufosHolder;
            _asteroidsHolder = asteroidsHolder;
            _bulletsHolder = bulletsHolder;
        }

        public void Enter()
        {
            _hudProvider.HUD.CreateLoseWindow();
        
            _ufosHolder.DestroyAll();
            _asteroidsHolder.DestroyAll();
            _bulletsHolder.DestroyAll();
        }

        public void Exit()
        {
            _hudProvider.HUD.Destroy();
        }
    }
}