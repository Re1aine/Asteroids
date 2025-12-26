using R3;

namespace Code.Logic.Gameplay.Entities.Enemy.UFO
{
    public class UFOModel
    {
        public ReadOnlyReactiveProperty<UfoConfig> Config => _config;
        
        private readonly ReactiveProperty<UfoConfig> _config = new();
        
        public UFOModel(UfoConfig config)
        {
            _config.Value = config;
        }
    }
}