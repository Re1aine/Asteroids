using System;
using Code.Logic.Gameplay.Services.Holders.RepositoriesHolder;
using Code.Logic.Services.Repository.Player;
using R3;

namespace Code.Logic.Gameplay.Services.ScoreCounter
{
    public class ScoreCountService : IScoreCountService, IDisposable
    {
        public ReadOnlyReactiveProperty<int> HighScore => _playerRepository.HighScore;
        
        public ReadOnlyReactiveProperty<int> Score => _score;
        private readonly ReactiveProperty<int> _score = new(0);

        private readonly PlayerRepository _playerRepository;

        private readonly CompositeDisposable _disposables = new();
        
        public ScoreCountService(IRepositoriesHolder repositoriesHolder)
        {
            _playerRepository = repositoriesHolder.GetRepository<PlayerRepository>();
         
            _score
                .Subscribe(SetHighScore)
                .AddTo(_disposables);
        }
        
        private void SetHighScore(int value)
        {
            if (value > _playerRepository.HighScore.CurrentValue) 
                _playerRepository.SetHighScore(value);
        }
        
        public void Add(int value) =>
            _score.Value += value;
        
        public void Reset() =>
            _score.Value = 0;

        public void Dispose() => 
            _disposables.Dispose();
    }
}