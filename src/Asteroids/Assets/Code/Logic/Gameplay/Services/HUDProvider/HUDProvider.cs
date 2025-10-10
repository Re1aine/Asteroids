using Code.Logic.Gameplay.Services.GameFactory;
using Code.UI;
using Code.UI.HUD;

namespace Code.Logic.Gameplay.Services.HUDProvider
{
    public class HUDProvider : IHUDProvider
    {
        public HUDPresenter HUD { get; private set; }
    
        private readonly IGameFactory _gameFactory;

        public HUDProvider(IGameFactory gameFactory) => 
            _gameFactory = gameFactory;

        public void Initialize() => 
            HUD = _gameFactory.CreateHUD();
    }
}