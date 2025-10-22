using System;
using Code.Logic.Gameplay.Services.SaveLoad;
using Code.Logic.Gameplay.Services.ScoreCounter;
using R3;

namespace Code.Logic.Gameplay.Services.Repository.Player
{
    public class PlayerRepository : IRepository, IDisposable
    {
        private readonly ISaveLoadService _saveLoadService;
        private readonly IScoreCountService _scoreCountService;
    
        private readonly PlayerSaveData _playerSaveData = new();

        public R3.ReadOnlyReactiveProperty<int> HighScore => _highScore;
        private readonly R3.ReactiveProperty<int> _highScore = new();
        
        private readonly CompositeDisposable _disposables = new();
        
        public PlayerRepository(ISaveLoadService saveLoadService, IScoreCountService scoreCountService)
        {
            _saveLoadService = saveLoadService;
            _scoreCountService = scoreCountService;
            
            HighScore
                .Subscribe(highScore => _playerSaveData.HighScore = highScore)
                .AddTo(_disposables);
        }
        
        public void Load() => 
            _highScore.Value = _saveLoadService.GetPlayerData(new PlayerSaveData()).HighScore;

        public void Delete()
        {
            _saveLoadService.SetPlayerData(_playerSaveData);
            
            _highScore.Value = 0;
        }

        public void Update()
        {
            if(_scoreCountService.Score.CurrentValue <= _playerSaveData.HighScore)
                return;
            
            _highScore.Value = _scoreCountService.Score.CurrentValue;
        }

        public void Save()
        {
            _saveLoadService.SetPlayerData(_playerSaveData);
            
            _saveLoadService.Save();
        }

        public void Dispose() => 
            _disposables.Dispose();
    }
}