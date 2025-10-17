
namespace Code.UI.LoseWindow
{
    public class LoseWindowModel
    {
        public readonly R3.ReadOnlyReactiveProperty<int> Score;
        
        private readonly R3.ReactiveProperty<int> _score;
        
        public LoseWindowModel()
        {
            _score = new R3.ReactiveProperty<int>(0);

            Score = _score.ToReadOnlyReactiveProperty();
        }

        public void SetScore(int value) => 
            _score.Value = value;
    }
}