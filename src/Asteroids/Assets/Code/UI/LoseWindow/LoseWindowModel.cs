using System;
using Code.Logic.Gameplay.Services.Holders.RepositoriesHolder;
using Code.Logic.Gameplay.Services.Repository.Player;
using Code.Logic.Gameplay.Services.ScoreCounter;
using R3;

namespace Code.UI.LoseWindow
{
    public class LoseWindowModel : IDisposable
    {
        public ReadOnlyReactiveProperty<int> Score => _score;
        public ReactiveProperty<int> HighScore => _highScore;

        private readonly ReactiveProperty<int> _score = new();
        private readonly ReactiveProperty<int> _highScore = new();

        private readonly CompositeDisposable _disposables = new();

        public LoseWindowModel(IScoreCountService scoreCountService, IRepositoriesHolder repositoriesHolder)
        {
            repositoriesHolder.GetRepository<PlayerRepository>().HighScore
                .Subscribe(SetHighScore)
                .AddTo(_disposables);
            
            scoreCountService.Score.
                Subscribe(SetScore)
                .AddTo(_disposables); ;
        }
        
        private void SetScore(int value) => 
            _score.Value = value;

        private void SetHighScore(int value) => 
            _highScore.Value = value;

        public void Dispose() => 
            _disposables.Dispose();
    }
}