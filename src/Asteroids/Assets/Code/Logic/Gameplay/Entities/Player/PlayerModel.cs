using R3;

namespace Code.Logic.Gameplay.Entities.Player
{
    public class PlayerModel
    {
        public ReadOnlyReactiveProperty<PlayerConfig> PlayerConfig => _playerConfig;
        private readonly ReactiveProperty<PlayerConfig> _playerConfig = new();
        public ReadOnlyReactiveProperty<GunConfig> GunConfig => _gunConfig;
        private readonly ReactiveProperty<GunConfig> _gunConfig = new();
        
        public PlayerModel(PlayerConfig playerConfig, GunConfig gunConfig)
        {
            _playerConfig.Value = playerConfig;
            _gunConfig.Value = gunConfig;
        }
    }
}