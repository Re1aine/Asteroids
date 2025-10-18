using Code.Logic.Gameplay.Services.ScoreCounter;

namespace Code.Logic.Gameplay.Services.Repository.Player
{
    public class PlayerRepository : IRepository
    {
        private const string HighScoreKey = "HighScore";

        public PlayerSaveData PlayerSaveData => _playerSaveData;
    
        private readonly ISaveLoadService _saveLoadService;
        private readonly IScoreCountService _scoreCountService;
    
        private PlayerSaveData _playerSaveData;
    
        public PlayerRepository(ISaveLoadService saveLoadService, IScoreCountService scoreCountService)
        {
            _saveLoadService = saveLoadService;
            _scoreCountService = scoreCountService;
        }

        public void Load()
        {
            _playerSaveData = new PlayerSaveData()
            {
                HighScore = _saveLoadService.GetInt(HighScoreKey, 0),
            };
        }

        public void Delete() => 
            _saveLoadService.SetInt(HighScoreKey, 0);

        public void Update()
        {
            if(_scoreCountService.Score <= _playerSaveData.HighScore)
                return;
        
            _playerSaveData.HighScore = _scoreCountService.Score;
        }

        public void Save()
        {
            _saveLoadService.SetInt(HighScoreKey, _playerSaveData.HighScore);
            _saveLoadService.Save();
        }
    }
}