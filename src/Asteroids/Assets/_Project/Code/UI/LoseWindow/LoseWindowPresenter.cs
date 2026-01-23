using R3;

namespace _Project.Code.UI.LoseWindow
{
    public class LoseWindowPresenter
    {
        public LoseWindowModel Model {get; }
        public LoseWindowView View {get; }
        
        private readonly CompositeDisposable _disposables = new();

        public LoseWindowPresenter(LoseWindowModel loseWindowModel, LoseWindowView loseWindowView)
        {
            Model = loseWindowModel;
            View = loseWindowView;
            
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
            Model.Dispose();
            View.Destroy();
        }
    }
}