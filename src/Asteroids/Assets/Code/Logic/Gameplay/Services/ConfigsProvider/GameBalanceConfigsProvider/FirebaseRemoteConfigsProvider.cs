using System;
using Code.Logic.Gameplay.Services.ConfigsProvider.Configs.GameBalance;
using Code.Logic.Gameplay.Services.SDKInitializer;
using Cysharp.Threading.Tasks;
using Firebase.RemoteConfig;
using UnityEngine;

namespace Code.Logic.Gameplay.Services.ConfigsProvider.GameBalanceConfigsProvider
{
    public class FirebaseRemoteConfigsProvider : IGameBalanceConfigsProvider
    {
        private const string PlayerConfigKey = "PlayerConfig";
        private const string AsteroidConfigKey = "AsteroidConfig";
        private const string GunConfigKey = "GunConfig";
        private const string UfoConfigKey = "UfoConfig";
        private const string UfoSpawnerConfigKey = "UfoSpawnerConfig";
        private const string AsteroidSpawnerConfigKey = "AsteroidSpawnerConfig";
    
        private readonly ISDKInitializer _sdkInitializer;
    
        public PlayerConfig PlayerConfig { get; private set; }
        public AsteroidConfig AsteroidConfig {get; private set; }
        public GunConfig GunConfig {get; private set; }
        public UfoConfig UfoConfig {get; private set; }
        public UfoSpawnerConfig UfoSpawnerConfig { get; private set; }
        public AsteroidSpawnerConfig AsteroidSpawnerConfig { get; private set; }
    
        private bool _isInitialized;

        public FirebaseRemoteConfigsProvider(ISDKInitializer sdkInitializer)
        {
            _sdkInitializer = sdkInitializer;
        }
    
        public async UniTask Initialize()
        {
            if(_isInitialized)
                return;

            if (_sdkInitializer.IsFireBaseInitialized)
            {
                await FetchDataAsync();
                await FetchComplete();
                _isInitialized = true;
                Debug.Log("<b><color=green> [Remote Config initialized successfully] </color></b>");
            }
            else
            {
                _isInitialized = false;
                Debug.Log("<b><color=red> [Remote Config is not initialized, using defaults] </color></b>");
            }
        
            LoadConfigs();
        }
    
        private async UniTask FetchDataAsync()
        {
            await FirebaseRemoteConfig.DefaultInstance
                .FetchAsync(TimeSpan.Zero)
                .AsUniTask();
        }

        private async UniTask FetchComplete()
        {
            var remoteConfig = FirebaseRemoteConfig.DefaultInstance;
            var info = remoteConfig.Info;
        
            if(info.LastFetchStatus != LastFetchStatus.Success) {
                Debug.LogError($"Fetch was unsuccessful" +
                               $"\n{nameof(info.LastFetchStatus)}: {info.LastFetchStatus}");
            
                _isInitialized = false;
            }
        
            await remoteConfig.ActivateAsync().AsUniTask()
                .ContinueWith(
                    task => {
                        Debug.Log($"Remote data loaded and ready for use. Last fetch time {info.FetchTime}.");
                        _isInitialized = true;
                    });
        }
    
        private void LoadConfigs()
        {
            PlayerConfig = GetConfig<PlayerConfig>(PlayerConfigKey);
            AsteroidConfig = GetConfig<AsteroidConfig>(AsteroidConfigKey);
            UfoConfig = GetConfig<UfoConfig>(UfoConfigKey);
            GunConfig = GetConfig<GunConfig>(GunConfigKey);
            UfoSpawnerConfig = GetConfig<UfoSpawnerConfig>(UfoSpawnerConfigKey);
            AsteroidSpawnerConfig =  GetConfig<AsteroidSpawnerConfig>(AsteroidSpawnerConfigKey);        
        }
    
        private T GetConfig<T>(string key) where T : new()
        {
            return _isInitialized ? 
                JsonUtility.FromJson<T>(FirebaseRemoteConfig.DefaultInstance.GetValue(key).StringValue)
                : new T();
        }
    }
}