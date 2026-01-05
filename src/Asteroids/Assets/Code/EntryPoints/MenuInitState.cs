using Code.GameFlow.States;
using Code.Logic.Gameplay.Services.Providers.HUDProvider;
using Cysharp.Threading.Tasks;

public class MenuInitState : IState
{
    private readonly MenuStateMachine _menuStateMachine;
    private readonly IPurchaseService _purchaseService;
    private readonly IPurchaseCatalog _purchaseCatalog;
    private readonly PurchaseHandler _purchaseHandler;
    private readonly IHUDProvider _hudProvider;

    public MenuInitState(MenuStateMachine menuStateMachine,
        IPurchaseService purchaseService,
        IPurchaseCatalog purchaseCatalog,
        PurchaseHandler purchaseHandler,
        IHUDProvider hudProvider)
    {
        _menuStateMachine = menuStateMachine;
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