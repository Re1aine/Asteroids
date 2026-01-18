using Code.Infrastructure.Common.AssetsManagement;
using Code.Infrastructure.Common.AssetsManagement.AssetProvider;
using Code.Logic.Services.HUDProvider;
using Code.UI.HUD;
using Code.UI.HUD.Menu;
using Code.UI.MenuWindow;
using Code.UI.SelectSavesWindow;
using Cysharp.Threading.Tasks;
using VContainer;

namespace Code.UI.UIFactory.MenuUIFactory
{
    public class MenuUIFactory : IMenuUIFactory
    {
        private readonly IAddressablesAssetsProvider _addressablesAssetsProvider;
        private readonly IObjectResolver _resolver;

        public MenuUIFactory(IAddressablesAssetsProvider addressablesAssetsProvider, IObjectResolver resolver)
        {
            _addressablesAssetsProvider = addressablesAssetsProvider;
            _resolver = resolver;
        }
    
        public async UniTask<AHUDPresenter> CreateHUD()
        {
            MenuHUDView view =  await _addressablesAssetsProvider.Instantiate<MenuHUDView>(AssetsAddress.MenuHUD);
            
            MenuHUDModel model = new MenuHUDModel(new MenuHUDService(this));
            
            return new MenuHUDPresenter(model, view);
        }

        public async UniTask<MenuWindowPresenter> CreateMenuWindow()
        {
            MenuWindowView view = await _addressablesAssetsProvider.Instantiate<MenuWindowView>(
                AssetsAddress.MenuWindow,
                _resolver.Resolve<IHUDProvider>().HUD.View.transform);

            MenuWindowModel model = new MenuWindowModel();
        
            return new MenuWindowPresenter(model, view);
        }

        public async UniTask<SelectSavesWindowPresenter> CreateSelectSavesWindow()
        {
            SelectSavesWindowView view = await _addressablesAssetsProvider.Instantiate<SelectSavesWindowView>(
                AssetsAddress.SelectSavesWindow,
                _resolver.Resolve<IHUDProvider>().HUD.View.transform);
            
            SelectSavesWindowModel model = new SelectSavesWindowModel();
            
            return new SelectSavesWindowPresenter(model, view);
        }
    }
}