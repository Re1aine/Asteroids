
namespace Code.UI.LoseWindow
{
    public class LoseWindowModel
    {
        public R3.ReadOnlyReactiveProperty<int> Score => _score;
        public R3.ReactiveProperty<int> HighScore => _highScore;

        private readonly R3.ReactiveProperty<int> _score = new();
        private readonly R3.ReactiveProperty<int> _highScore = new();
        
        public void SetScore(int value) => 
            _score.Value = value;
        
        public void SetHighScore(int value) => 
            _highScore.Value = value;
    }
}