using Code.UI.HUD;
using Code.UI.UIFactory;
using Cysharp.Threading.Tasks;

namespace Code.Logic.Services.HUDProvider
{
    public class HUDProvider : IHUDProvider
    {
        public AHUDPresenter HUD { get; private set; }
    
        private readonly IUIFactory _uiFactory;

        public HUDProvider(IUIFactory uiFactory) => 
            _uiFactory = uiFactory;

        public async UniTask Initialize() => 
            HUD = await _uiFactory.CreateHUD();
    }
}