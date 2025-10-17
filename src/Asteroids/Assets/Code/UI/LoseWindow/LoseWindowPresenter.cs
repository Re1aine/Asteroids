using Code.Logic.Gameplay.Services.ScoreCounter;
using R3;

namespace Code.UI.LoseWindow
{
    public class LoseWindowPresenter
    {
        public LoseWindowModel LoseWindowModel {get; private set;}
        public LoseWindowView LoseWindowView {get; private set;}
    
        private IScoreCountService _scoreCountService;

        private readonly CompositeDisposable _disposables = new();

        public LoseWindowPresenter(LoseWindowModel loseWindowModel, LoseWindowView loseWindowView)
        {
            LoseWindowModel = loseWindowModel;
            LoseWindowView = loseWindowView;
        }

        public void Init(IScoreCountService scoreCountService)
        {
            _scoreCountService = scoreCountService;
            
            LoseWindowModel.Score
                .Subscribe(score => LoseWindowView.SetScore(score))
                .AddTo(_disposables);
            
            SetScore(_scoreCountService.Score);
        }

        private void SetScore(int value) => 
            LoseWindowModel.SetScore(value);

        public void Destroy()
        {
            _disposables.Dispose();
            LoseWindowView.Destroy();
        }
    }
}