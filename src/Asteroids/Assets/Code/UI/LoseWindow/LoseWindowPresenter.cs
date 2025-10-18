using Code.Logic.Gameplay.Services.Holders.RepositoriesHolder;
using Code.Logic.Gameplay.Services.Repository.Player;
using Code.Logic.Gameplay.Services.ScoreCounter;
using R3;

namespace Code.UI.LoseWindow
{
    public class LoseWindowPresenter
    {
        public LoseWindowModel Model {get; private set;}
        public LoseWindowView View {get; private set;}
    
        private IScoreCountService _scoreCountService;

        private readonly CompositeDisposable _disposables = new();
        private IRepositoriesHolder _repositoriesHolder;

        public LoseWindowPresenter(LoseWindowModel loseWindowModel, LoseWindowView loseWindowView)
        {
            Model = loseWindowModel;
            View = loseWindowView;
        }

        public void Init(IScoreCountService scoreCountService, IRepositoriesHolder repositoriesHolder)
        {
            _scoreCountService = scoreCountService;
            _repositoriesHolder = repositoriesHolder;

            _repositoriesHolder.GetRepository<PlayerRepository>().HighScore
                .Subscribe(highScore => Model.SetHighScore(highScore))
                .AddTo(_disposables);
            
            _scoreCountService.Score.
                Subscribe(score => Model.SetScore(score))
                .AddTo(_disposables);
            
            Model.Score
                .Subscribe(score => View.SetScore(score))
                .AddTo(_disposables);
            
            Model.HighScore
                .Subscribe(highScore => View.SetHighScore(highScore))
                .AddTo(_disposables);
        }
        
        public void Destroy()
        {
            _disposables.Dispose();
            View.Destroy();
        }
    }
}