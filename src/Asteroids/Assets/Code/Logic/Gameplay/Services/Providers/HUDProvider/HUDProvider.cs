using Code.UI.HUD;
using Cysharp.Threading.Tasks;

namespace Code.Logic.Gameplay.Services.Providers.HUDProvider
{
    public class HUDProvider : IHUDProvider
    {
        public AHUDPresenter HUD { get; private set; }
    
        private readonly IUIFactory _uiFactory;

        public HUDProvider(IUIFactory gameplayUIFactory) => 
            _uiFactory = gameplayUIFactory;

        public async UniTask Initialize() => 
            HUD = await _uiFactory.CreateHUD();
    }
}