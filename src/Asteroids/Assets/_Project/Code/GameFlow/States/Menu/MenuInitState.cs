using _Project.Code.Logic.Menu.Services.Purchase;
using _Project.Code.Logic.Menu.Services.Purchase.Catalog;
using _Project.Code.Logic.Menu.Services.Purchase.Handler;
using _Project.Code.Logic.Services.HUDProvider;
using _Project.Code.Logic.Services.SaveLoad;
using Cysharp.Threading.Tasks;

namespace _Project.Code.GameFlow.States.Menu
{
    public class MenuInitState : IState
    {
        private readonly MenuStateMachine _menuStateMachine;
        private readonly IPurchaseService _purchaseService;
        private readonly IPurchaseCatalog _purchaseCatalog;
        private readonly PurchaseHandler _purchaseHandler;
        private readonly IHUDProvider _hudProvider;
        private readonly ISaveLoadService _saveLoadService;

        public MenuInitState(
            MenuStateMachine menuStateMachine,
            IPurchaseService purchaseService,
            IPurchaseCatalog purchaseCatalog,
            PurchaseHandler purchaseHandler,
            IHUDProvider hudProvider,
            ISaveLoadService saveLoadService)
        {
            _menuStateMachine = menuStateMachine;
            _purchaseService = purchaseService;
            _purchaseCatalog = purchaseCatalog;
            _purchaseHandler = purchaseHandler;
            _hudProvider = hudProvider;
            _saveLoadService = saveLoadService;
        }

        public async UniTask Enter()
        {
            await _saveLoadService.Preload();

            _purchaseCatalog.Initialize();
            _purchaseService.Initialize();
            _purchaseHandler.Initialize();
            
            await _hudProvider.Initialize();

            if (_saveLoadService.HasConflict) 
                _menuStateMachine.Enter<SelectSavesState>().Forget();
            else
            {
                _saveLoadService.ResolveAutomatically();
                _menuStateMachine.Enter<MenuStartState>().Forget();
            }
        }

        public UniTask Exit() => default;
    }
}