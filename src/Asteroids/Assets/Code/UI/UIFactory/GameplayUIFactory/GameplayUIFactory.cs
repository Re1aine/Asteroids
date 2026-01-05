using Code.Infrastructure.Common.AssetsManagement;
using Code.Infrastructure.Common.AssetsManagement.AssetProvider;
using Code.Logic.Gameplay.Services.AdService;
using Code.Logic.Gameplay.Services.Factories.GameFactory;
using Code.Logic.Gameplay.Services.Providers.HUDProvider;
using Code.Logic.Gameplay.Services.Providers.PlayerProvider;
using Code.Logic.Gameplay.Services.ScoreCounter;
using Code.UI.HUD;
using Code.UI.LoseWindow;
using Code.UI.PlayerStatsWindow;
using Cysharp.Threading.Tasks;
using VContainer;

public class GameplayUIFactory : IGameplayUIFactory
{
    private readonly IAddressablesAssetsProvider _addressablesAssetsProvider;
    private readonly IScoreCountService _scoreCountService;
    private readonly IObjectResolver _resolver;

    public GameplayUIFactory(
        IAddressablesAssetsProvider addressablesAssetsProvider,
        IScoreCountService scoreCountService,
        IObjectResolver resolver)
    {
        _addressablesAssetsProvider = addressablesAssetsProvider;
        _scoreCountService = scoreCountService;
        _resolver = resolver;
    }
    
    public async UniTask<AHUDPresenter> CreateHUD()
    {
        GameplayHUDView view =  await _addressablesAssetsProvider.Instantiate<GameplayHUDView>(
            AssetsAddress.GameplayHUD);
            
        GameplayHUDModel model = new GameplayHUDModel(new GameplayHUDService(this));
            
        return new GameplayHUDPresenter(model, view);
    }
    
    public async UniTask<LoseWindowPresenter> CreateLoseWindow()
    {
        LoseWindowView view =  await _addressablesAssetsProvider.Instantiate<LoseWindowView>(
            AssetsAddress.LoseWindow,
            _resolver.Resolve<IHUDProvider>().HUD.View.transform);
            
        LoseWindowModel model = new LoseWindowModel(
            _scoreCountService);
            
        return new LoseWindowPresenter(model, view);
    }

    public async UniTask<PlayerStatsWindowPresenter> CreatePlayerStatsWindow()
    {
        PlayerStatsWindowView view = await _addressablesAssetsProvider.Instantiate<PlayerStatsWindowView>(
            AssetsAddress.PlayerStatsWindow,
            _resolver.Resolve<IHUDProvider>().HUD.View.transform);
            
        PlayerStatsWindowModel model = new PlayerStatsWindowModel(
            _resolver.Resolve<IPlayerProvider>());
            
        return new PlayerStatsWindowPresenter(model, view);
    }
    
    public async UniTask<ReviveWindowPresenter> CreateReviveWindow()
    {
        ReviveWindowView view = await _addressablesAssetsProvider.Instantiate<ReviveWindowView>(
            AssetsAddress.ReviveWindow, 
            _resolver.Resolve<IHUDProvider>().HUD.View.transform);

        ReviveWindowModel model = new ReviveWindowModel(_resolver.Resolve<IAdsService>());
            
        return new ReviveWindowPresenter(model, view);
    }
}