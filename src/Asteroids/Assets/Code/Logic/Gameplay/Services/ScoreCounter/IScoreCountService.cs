namespace Code.Logic.Gameplay.Services.ScoreCounter
{
    public interface IScoreCountService
    {
        R3.ReadOnlyReactiveProperty<int> Score { get; }
        void Add(int value);
        void Reset();
    }
}