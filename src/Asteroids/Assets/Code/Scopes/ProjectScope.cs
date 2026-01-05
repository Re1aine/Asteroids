using Code.GameFlow;
using Code.GameFlow.States.Core;
using Code.Infrastructure.Common.AssetsManagement.AssetLoader;
using Code.Infrastructure.Common.CoroutineService;
using Code.Logic.Gameplay.Services.Holders.RepositoriesHolder;
using Code.Logic.Gameplay.Services.Repository;
using Code.Logic.Gameplay.Services.Repository.Player;
using Code.Logic.Gameplay.Services.SaveLoad;
using Code.Logic.Gameplay.Services.SDKInitializer;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class ProjectScope : LifetimeScope
{
    [SerializeField] private CoroutineRunner _coroutineRunner;

    protected override void Awake()
    {
        DontDestroyOnLoad(this);
        base.Awake();
    }

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponentInNewPrefab(_coroutineRunner, Lifetime.Singleton).As<ICoroutineRunner>();

        builder.Register<AddressablesAssetsLoader>(Lifetime.Singleton).As<IAddressablesAssetsLoader>();

        builder.Register<SaveLoadService>(Lifetime.Singleton).As<ISaveLoadService>();
        
        builder.Register<PlayerRepository>(Lifetime.Singleton).As<IRepository>();
        builder.Register<RepositoriesHolder>(Lifetime.Singleton).As<IRepositoriesHolder>();

        builder.Register<SDKInitializer>(Lifetime.Singleton).As<ISDKInitializer>();
        
        builder.Register<SceneLoader>(Lifetime.Singleton).As<ISceneLoader>();

        builder.Register<StateFactory>(Lifetime.Singleton);
        builder.Register<GameStateMachine>(Lifetime.Singleton);

        builder.RegisterEntryPoint<ProjectEntryPoint>();
    }
}