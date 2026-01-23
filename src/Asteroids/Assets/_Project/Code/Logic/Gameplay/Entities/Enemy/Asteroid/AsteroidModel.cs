using _Project.Code.Logic.Gameplay.Services.Configs.Configs.Balance;
using R3;

namespace _Project.Code.Logic.Gameplay.Entities.Enemy.Asteroid
{
    public class AsteroidModel
    {
        public AsteroidType AsteroidType { get; }

        public ReadOnlyReactiveProperty<AsteroidConfig> Config => _config;
        
        private readonly ReactiveProperty<AsteroidConfig> _config = new();
        
        public AsteroidModel(AsteroidType asteroidType, AsteroidConfig config)
        {
            AsteroidType = asteroidType;
            _config.Value = config;
        }
    }
}