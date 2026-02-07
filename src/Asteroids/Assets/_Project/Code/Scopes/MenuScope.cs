using _Project.Code.EntryPoints;
using _Project.Code.GameFlow;
using _Project.Code.GameFlow.States.Menu;
using _Project.Code.Infrastructure.Common.AssetsManagement.AssetProvider;
using _Project.Code.Logic.Gameplay.Audio;
using _Project.Code.Logic.Menu;
using _Project.Code.Logic.Menu.Services.Purchase;
using _Project.Code.Logic.Menu.Services.Purchase.Catalog;
using _Project.Code.Logic.Menu.Services.Purchase.Handler;
using _Project.Code.Logic.Services.HUDProvider;
using _Project.Code.UI.UIFactory;
using _Project.Code.UI.UIFactory.MenuUIFactory;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Code.Scopes
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