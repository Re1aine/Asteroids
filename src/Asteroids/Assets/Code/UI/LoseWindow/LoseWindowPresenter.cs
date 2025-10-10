using Code.Logic.Gameplay.Services.ScoreCounter;

namespace Code.UI.LoseWindow
{
    public class LoseWindowPresenter
    {
        public LoseWindowModel LoseWindowModel {get; private set;}
        public LoseWindowView LoseWindowView {get; private set;}
    
        private IScoreCountService _scoreCountService;

        public LoseWindowPresenter(LoseWindowModel loseWindowModel, LoseWindowView loseWindowView)
        {
            LoseWindowModel = loseWindowModel;
            LoseWindowView = loseWindowView;
        }

        public void Init(IScoreCountService scoreCountService)
        {
            _scoreCountService = scoreCountService;
        
            LoseWindowModel.Score.OnValueChanged += LoseWindowView.SetScore;
        
            SetScore(_scoreCountService.Score);
        }

        private void SetScore(int value) => 
            LoseWindowModel.SetScore(value);

        public void Destroy()
        {
            LoseWindowModel.Score.OnValueChanged -= LoseWindowView.SetScore;
            LoseWindowView.Destroy();
        }
    }
}