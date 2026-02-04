using R3;

namespace _Project.Code.Logic.Gameplay.Services.ScoreCounter
{
    public interface IScoreCountService
    {
        ReadOnlyReactiveProperty<int> HighScore { get; }
        ReadOnlyReactiveProperty<int> Score { get; }
        void Add(int value);
        void Reset();
    }
}