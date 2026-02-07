using _Project.Code.EntryPoints;
using _Project.Code.GameFlow;
using _Project.Code.GameFlow.States.Core;
using _Project.Code.Infrastructure.Common.AssetsManagement.AssetLoader;
using _Project.Code.Infrastructure.Common.CoroutineService;
using _Project.Code.Infrastructure.Common.LogService;
using _Project.Code.Infrastructure.Common.SceneLoader;
using _Project.Code.Logic.Gameplay.Audio;
using _Project.Code.Logic.Gameplay.Services.Configs.AssetsConfigProvider;
using _Project.Code.Logic.Gameplay.Services.Holders.RepositoriesHolder;
using _Project.Code.Logic.Services.Authentification;
using _Project.Code.Logic.Services.Repository.Player;
using _Project.Code.Logic.Services.SaveLoad;
using _Project.Code.Logic.Services.SaveLoad.CloudStrategy;
using _Project.Code.Logic.Services.SaveLoad.LocalStrategy;
using _Project.Code.Logic.Services.SaveLoad.LocalStrategy.Storage;
using _Project.Code.Logic.Services.SDKInitializer;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Code.Scopes
{
    public class ProjectScope : LifetimeScope
    {
        [SerializeField] private CoroutineRunner _coroutineRunner;
        
        [SerializeField] private AudioPlayer _audioPlayer;

        protected override void Awake()
        {
            DontDestroyOnLoad(this);
            base.Awake();
        }

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<LogService>(Lifetime.Singleton).As<ILogService>();
            
            builder.RegisterComponentInNewPrefab(_coroutineRunner, Lifetime.Singleton).As<ICoroutineRunner>();
            builder.RegisterComponentInNewPrefab(_audioPlayer, Lifetime.Singleton).As<AudioPlayer>();
            
            builder.Register<AddressablesAssetsLoader>(Lifetime.Singleton).As<IAddressablesAssetsLoader>();

            builder.Register<AssetsConfigsProvider>(Lifetime.Singleton).As<IAssetsConfigsProvider>();
            
            builder.Register<SDKInitializer>(Lifetime.Singleton).As<ISDKInitializer>();
            
            builder.Register<GamePushAuthentification>(Lifetime.Singleton).As<IAuthentification>();

            builder.Register<LocalSaveLoadStorage>(Lifetime.Singleton).As<ILocalSaveLoadStorage>();
            builder.Register<LocalSaveLoadStrategy>(Lifetime.Singleton).As<ILocalSaveLoadStrategy>();
            builder.Register<CloudSaveLoadStrategy>(Lifetime.Singleton).As<ICloudSaveLoadStrategy>();
            builder.Register<SaveLoadService>(Lifetime.Singleton).As<ISaveLoadService>();
            
            builder.Register<PlayerRepository>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<RepositoriesHolder>(Lifetime.Singleton).As<IRepositoriesHolder>();
            
            builder.Register<SceneLoader>(Lifetime.Singleton).As<ISceneLoader>();
            
            builder.Register<AudioService>(Lifetime.Singleton).As<IAudioService>();

            builder.Register<StateFactory>(Lifetime.Singleton);
            builder.Register<GameStateMachine>(Lifetime.Singleton);

            builder.RegisterEntryPoint<ProjectEntryPoint>();
        }
    }
}