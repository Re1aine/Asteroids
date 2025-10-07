public interface IScoreCountService
{
    int Score { get; }
    void Add(int value);
    void Reset();
}