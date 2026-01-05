using Code.Logic.Menu.Services.Purchase;
using Code.Logic.Menu.Services.Purchase.Catalog;
using Code.Logic.Menu.Services.Purchase.Handler;
using Code.Logic.Services.HUDProvider;
using Code.UI;
using Cysharp.Threading.Tasks;

namespace Code.GameFlow.States.Menu
{
    public class MenuInitState : IState
    {
        private readonly IPurchaseService _purchaseService;
        private readonly IPurchaseCatalog _purchaseCatalog;
        private readonly PurchaseHandler _purchaseHandler;
        private readonly IHUDProvider _hudProvider;
        
        public MenuInitState(
            IPurchaseService purchaseService,
            IPurchaseCatalog purchaseCatalog,
            PurchaseHandler purchaseHandler,
            IHUDProvider hudProvider)
        {
            _purchaseService = purchaseService;
            _purchaseCatalog = purchaseCatalog;
            _purchaseHandler = purchaseHandler;
            _hudProvider = hudProvider;
        }

        public async UniTask Enter()
        {
            _purchaseCatalog.Initialize();
            _purchaseService.Initialize();
            _purchaseHandler.Initialize();
        
            await _hudProvider.Initialize();
        
            _hudProvider.HUD.ShowWindow(WindowType.MenuWindow);
        }

        public UniTask Exit() => default;
    }
}