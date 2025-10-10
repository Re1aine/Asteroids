
using Code.Logic.Gameplay.Services.AsteroidsHolder;
using Code.Logic.Gameplay.Services.BulletsHolder;
using Code.Logic.Gameplay.Services.GameFactory;
using Code.Logic.Gameplay.Services.HUDProvider;
using Code.Logic.Gameplay.Services.ScoreCounter;
using Code.Logic.Gameplay.Services.UFOsHolder;
using Code.UI;

namespace Code.GameFlow.States.Gameplay
{
    public class LoseState : IState
    {
        private readonly IGameFactory _gameFactory;
        private readonly IHUDProvider _hudProvider;
        private readonly IUFOsHolder _ufosHolder;
        private readonly IAsteroidsHolder _asteroidsHolder;
        private readonly IBulletsHolder _bulletsHolder;
        private readonly IScoreCountService _scoreCountService;

        private LoseWindowPresenter _loseWindow;

        public LoseState(IGameFactory gameFactory, IHUDProvider hudProvider, IUFOsHolder ufosHolder, IAsteroidsHolder  asteroidsHolder, IBulletsHolder bulletsHolder)
        {
            _gameFactory = gameFactory;
            _hudProvider = hudProvider;
            _ufosHolder = ufosHolder;
            _asteroidsHolder = asteroidsHolder;
            _bulletsHolder = bulletsHolder;
        }

        public void Enter()
        {
            _loseWindow = _gameFactory.CreateLoseWindow(_hudProvider.HUD.transform);
        
            _ufosHolder.DestroyAll();
            _asteroidsHolder.DestroyAll();
            _bulletsHolder.DestroyAll();
        }

        public void Exit()
        {
            _loseWindow.Destroy();
        }
    }
}