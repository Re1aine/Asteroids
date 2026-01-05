using Code.GameFlow;
using Code.Infrastructure.Common.AssetsManagement.AssetProvider;
using Code.Logic.Gameplay.Services.Providers.HUDProvider;
using VContainer;
using VContainer.Unity;

public class MenuScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<AddressablesAssetsProvider>(Lifetime.Singleton).As<IAddressablesAssetsProvider>();
        
        builder.Register<PurchaseCatalog>(Lifetime.Singleton).As<IPurchaseCatalog>();
        builder.Register<GamePushPurchaseService>(Lifetime.Singleton).As<IPurchaseService>();
        builder.Register<PurchaseHandler>(Lifetime.Singleton).AsSelf();
        
        builder.Register<MenuUIFactory>(Lifetime.Singleton)
            .As<IMenuUIFactory>()
            .As<IUIFactory>();
        
        builder.Register<HUDProvider>(Lifetime.Singleton).As<IHUDProvider>();
        
        builder.Register<StateFactory>(Lifetime.Singleton);
        builder.Register<MenuStateMachine>(Lifetime.Singleton);
        
        builder.RegisterEntryPoint<MenuEntryPoint>();
    }
}