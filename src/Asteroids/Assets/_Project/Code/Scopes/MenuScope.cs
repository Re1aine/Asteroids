using Code.EntryPoints;
using Code.GameFlow;
using Code.GameFlow.States.Menu;
using Code.Infrastructure.Common.AssetsManagement.AssetProvider;
using Code.Logic.Menu;
using Code.Logic.Menu.Services.Purchase;
using Code.Logic.Menu.Services.Purchase.Catalog;
using Code.Logic.Menu.Services.Purchase.Handler;
using Code.Logic.Services.HUDProvider;
using Code.UI.UIFactory;
using Code.UI.UIFactory.MenuUIFactory;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code.Scopes
{
    public class MenuScope : LifetimeScope
    {
        [SerializeField] private SecretCodeDeliverer _codeDeliverer;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<AddressablesAssetsProvider>(Lifetime.Singleton).As<IAddressablesAssetsProvider>();
            
            builder.RegisterComponentInNewPrefab(_codeDeliverer, Lifetime.Singleton);
            
            builder.Register<AuthHandler>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            
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
}