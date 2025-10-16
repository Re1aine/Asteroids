using System;
using R3;

namespace Code.UI.LoseWindow
{
    public class LoseWindowModel : IDisposable
    {
        public readonly R3.ReadOnlyReactiveProperty<int> Score;
        
        private readonly R3.ReactiveProperty<int> _score;
        
        private readonly CompositeDisposable _disposables = new();
        
        public LoseWindowModel()
        {
            _score = new R3.ReactiveProperty<int>(0)
                .AddTo(_disposables);

            Score = _score.ToReadOnlyReactiveProperty()
                .AddTo(_disposables);
        }

        public void SetScore(int value) => 
            _score.Value = value;

        public void Dispose() => 
            _disposables.Dispose();
    }
}