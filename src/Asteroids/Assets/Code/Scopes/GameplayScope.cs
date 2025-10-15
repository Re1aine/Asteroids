using Code.EntryPoints;
using Code.GameFlow;
using Code.GameFlow.States.Gameplay;
using Code.Infrastructure.Common;
using Code.Infrastructure.Common.AssetsManagement.AssetsLoader;
using Code.Infrastructure.Common.AssetsManagement.AssetsProvider;
using Code.Infrastructure.Common.CoroutineService;
using Code.Logic.Gameplay;
using Code.Logic.Gameplay.Services.Boundries;
using Code.Logic.Gameplay.Services.Factories.GameFactory;
using Code.Logic.Gameplay.Services.Holders.AsteroidsHolder;
using Code.Logic.Gameplay.Services.Holders.BulletsHolder;
using Code.Logic.Gameplay.Services.Holders.UFOsHolder;
using Code.Logic.Gameplay.Services.Input;
using Code.Logic.Gameplay.Services.Observers.PlayerDeathObserver;
using Code.Logic.Gameplay.Services.PointWrapper;
using Code.Logic.Gameplay.Services.Providers.CameraProvider;
using Code.Logic.Gameplay.Services.Providers.HUDProvider;
using Code.Logic.Gameplay.Services.Providers.PlayerProvider;
using Code.Logic.Gameplay.Services.ScoreCounter;
using Code.Logic.Gameplay.Services.Spawners.AsteroidsSpawner;
using Code.Logic.Gameplay.Services.Spawners.UFOsSpawner;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code.Scopes
{
    public class GameplayScope : LifetimeScope
    {
        [SerializeField] private Camera _camera;
    
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<AssetsLoader>(Lifetime.Singleton).As<IAssetsLoader>();
            builder.Register<AssetsProvider>(Lifetime.Singleton).As<IAssetsProvider>();
        
            builder.RegisterComponentInHierarchy<CoroutineRunner>().As<ICoroutineRunner>();
        
            builder.Register<InputService>(Lifetime.Singleton).As<IInputService>();

            builder.Register<CameraProvider>(Lifetime.Singleton).As<ICameraProvider>().WithParameter(_camera);
            builder.Register<PlayerProvider>(Lifetime.Singleton).As<IPlayerProvider>();
            builder.Register<HUDProvider>(Lifetime.Singleton).As<IHUDProvider>();
        
            builder.Register<ScreenBoundaries>(Lifetime.Singleton).As<IBoundaries>();
            builder.Register<PointWrapService>(Lifetime.Singleton).As<IPointWrapService>();

            builder.Register<ScoreCountService>(Lifetime.Singleton).As<IScoreCountService>();
        
            builder.Register<GameFactory>(Lifetime.Singleton).As<IGameFactory>();

            builder.Register<PlayerDeathObserver>(Lifetime.Singleton).As<IPlayerDeathObserver>();

            builder.Register<UFOsHolder>(Lifetime.Singleton).As<IUFOsHolder>();
            builder.Register<AsteroidsHolder>(Lifetime.Singleton).As<IAsteroidsHolder>();
            builder.Register<BulletsHolder>(Lifetime.Singleton).As<IBulletsHolder>();
        
            builder.Register<UFOSpawner>(Lifetime.Singleton).As<IUFOSpawner>();
            builder.Register<AsteroidSpawner>(Lifetime.Singleton).As<IAsteroidSpawner>();
        
            builder.RegisterComponentInHierarchy<BackgroundResizer>();
        
            builder.Register<StateFactory>(Lifetime.Singleton);
            builder.Register<GameplayStateMachine>(Lifetime.Singleton);
        
            builder.RegisterEntryPoint<GameplayEntryPoint>();
        }
    }
}