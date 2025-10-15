using Code.Logic.Gameplay.Services.Factories.GameFactory;
using Code.Logic.Gameplay.Services.Providers.PlayerProvider;
using Code.Logic.Gameplay.Services.ScoreCounter;
using Code.UI.LoseWindow;
using Code.UI.PlayerStatsWindow;

namespace Code.UI.HUD
{
    public class HUDPresenter
    {
        public HUDModel Model { get; private set; }
        public HUDView View { get; private set; }
    
        private IPlayerProvider _playerProvider;
        private IGameFactory _gameFactory;
        private IScoreCountService _scoreCountService;

        private PlayerStatsWindowPresenter _playerStats;
        private LoseWindowPresenter _loseWindow;
        
        public HUDPresenter(HUDModel model, HUDView view)
        {
            Model = model;
            View = view;
        }

        public void Init(IGameFactory gameFactory, IScoreCountService scoreCountService, IPlayerProvider playerProvider)
        {
            _gameFactory = gameFactory;
            _scoreCountService = scoreCountService;
            _playerProvider = playerProvider;
        }

        public void CreateLoseWindow()
        {
            _loseWindow = _gameFactory.CreateLoseWindow(View.transform);
            _loseWindow.Init(_scoreCountService);
        }

        public void CreatePlayerStatsWindow()
        {
            _playerStats = _gameFactory.CreatePlayerStatsWindow(View.transform);
            _playerStats.Init(_playerProvider);
        }

        public void Destroy() => 
            View.Destroy();
        public void DestroyLoseWindow() =>
            _loseWindow.Destroy();
        public void DestroyPlayerStatsWindow() =>
            _playerStats.Destroy();
    }
}