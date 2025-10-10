public class LoseWindowModel
{
    public readonly ReadOnlyReactiveProperty<int> Score;

    private readonly ReactiveProperty<int> _score;

    public LoseWindowModel()
    {
        _score = new ReactiveProperty<int>(0);
        
        Score = new ReadOnlyReactiveProperty<int>(_score);
    }

    public void SetScore(int value) => _score.SetValue(value);
}