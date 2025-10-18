namespace Code.Logic.Gameplay.Services.ScoreCounter
{
    public class ScoreCountService : IScoreCountService
    {
        private readonly R3.ReactiveProperty<int> _score = new(0);
        
        public R3.ReadOnlyReactiveProperty<int> Score => _score;
        
        public void Add(int value) =>
            _score.Value += value;
        
        public void Reset() =>
            _score.Value = 0;
    }
}