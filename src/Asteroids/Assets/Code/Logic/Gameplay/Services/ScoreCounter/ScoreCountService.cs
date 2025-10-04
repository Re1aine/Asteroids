public class ScoreCountService : IScoreCountService
{
    public int Score => _score;

    private int _score;
    
    public void Add(int value) => _score += value;
}