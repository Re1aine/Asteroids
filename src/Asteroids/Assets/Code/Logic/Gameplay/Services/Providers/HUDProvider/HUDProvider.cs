using Code.Logic.Gameplay.Services.Factories.GameFactory;
using Code.UI.HUD;
using Cysharp.Threading.Tasks;

namespace Code.Logic.Gameplay.Services.Providers.HUDProvider
{
    public class HUDProvider : IHUDProvider
    {
        public HUDPresenter HUD { get; private set; }
    
        private readonly IGameFactory _gameFactory;

        public HUDProvider(IGameFactory gameFactory) => 
            _gameFactory = gameFactory;

        public async UniTask Initialize() => 
            HUD = await _gameFactory.CreateHUD();
    }
}