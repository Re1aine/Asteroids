using Code.Logic.Gameplay.Services.Configs.Configs.GameBalance;
using Code.Logic.Services.SDKInitializer;
using Cysharp.Threading.Tasks;
using GamePush;
using UnityEngine;

namespace Code.Logic.Gameplay.Services.Configs.GameBalanceConfigsProvider
{
    public class GamePushRemoteConfigsProvider : IGameBalanceConfigsProvider
    {
        private const string PlayerConfigKey = "PlayerConfig";
        private const string AsteroidConfigKey = "AsteroidConfig";
        private const string GunConfigKey = "GunConfig";
        private const string UfoConfigKey = "UfoConfig";
        private const string UfoSpawnerConfigKey = "UfoSpawnerConfig";
        private const string AsteroidSpawnerConfigKey = "AsteroidSpawnerConfig";
    
        public PlayerConfig PlayerConfig { get; private set; }
        public AsteroidConfig AsteroidConfig {get; private set; }
        public GunConfig GunConfig {get; private set; }
        public UfoConfig UfoConfig {get; private set; }
        public UfoSpawnerConfig UfoSpawnerConfig { get; private set; }
        public AsteroidSpawnerConfig AsteroidSpawnerConfig { get; private set; }
    
        private readonly ISDKInitializer _sdkInitializer;
    
        private bool _isInitialized;

        public GamePushRemoteConfigsProvider(ISDKInitializer sdkInitializer)
        {
            _sdkInitializer = sdkInitializer;
        }

        public UniTask Initialize()
        {
            if(_isInitialized)
                return default;

            if (_sdkInitializer.IsGamePushInitialized)
            {
                _isInitialized = true;
                Debug.Log("<b><color=green> [Remote Config initialized successfully] </color></b>");
            }
            else
            {
                _isInitialized = false;
                Debug.Log("<b><color=red> [Remote Config is not initialized, using defaults] </color></b>");
            }
        
            LoadConfigs();
            return default;
        }
    
        private bool IsCanFetch() => 
            _isInitialized;
        
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
            return IsCanFetch() && GP_Variables.Has(key) ? 
                JsonUtility.FromJson<T>(GP_Variables.GetString(key))
                : new T();
        }
    }
}