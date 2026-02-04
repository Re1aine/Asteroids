using _Project.Code.UI.HUD;
using _Project.Code.UI.UIFactory;
using Cysharp.Threading.Tasks;

namespace _Project.Code.Logic.Services.HUDProvider
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