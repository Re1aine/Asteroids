using Code.Logic.Gameplay.Services.Factories.GameFactory;
using Code.Logic.Gameplay.Services.Providers.PlayerProvider;
using Code.Logic.Gameplay.Services.ScoreCounter;
using Code.UI.LoseWindow;

namespace Code.UI.HUD
{
    public class HUDPresenter
    {
        public HUDModel Model { get; private set; }
        public HUDView View { get; private set; }
    
        private IPlayerProvider _playerProvider;
        private IGameFactory _gameFactory;
        private IScoreCountService _scoreCountService;

        public HUDPresenter(HUDModel model, HUDView view)
        {
            Model = model;
            View = view;

            view.OnPlayerStatsWindowCreated += CreatePlayerStatsWindow;
            model.PlayerStatsWindow.OnValueChanged += view.SetPlayerStatsWindow;
        }

        public void Init(IGameFactory gameFactory, IScoreCountService scoreCountService)
        {
            _gameFactory = gameFactory;
            _scoreCountService = scoreCountService;
        }

        public void Destroy()
        {
            View.Destroy();
        }

        public void CreateLoseWindow()
        {
            LoseWindowPresenter loseWindow = _gameFactory.CreateLoseWindow(View.transform);
            Model.SetLoseWindow(loseWindow);
            loseWindow.Init(_scoreCountService);
        }

        public void DestroyLoseWindow()
        {
            Model.LoseWindow.Value.Destroy();
        
            Model.SetLoseWindow(null);
        }
    
        private void CreatePlayerStatsWindow() => 
            Model.SetPlayerStatsWindow(_gameFactory.CreatePlayerStatsWindow(View.transform));

        public void DestroyPlayerStatsWindow()
        {
            View.OnPlayerStatsWindowCreated -= CreatePlayerStatsWindow;
        
            Model.PlayerStatsWindow.OnValueChanged -= View.SetPlayerStatsWindow;

            Model.PlayerStatsWindow.Value.Destroy();
        
            Model.SetPlayerStatsWindow(null);
        }
    }
}