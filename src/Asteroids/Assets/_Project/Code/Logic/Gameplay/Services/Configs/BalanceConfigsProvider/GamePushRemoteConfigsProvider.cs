using _Project.Code.Infrastructure.Common.LogService;
using _Project.Code.Logic.Gameplay.Services.Configs.Configs.Balance;
using _Project.Code.Logic.Services.SDKInitializer;
using Cysharp.Threading.Tasks;
using UnityEngine;
#if !UNITY_EDITOR
using GamePush;
#endif

namespace _Project.Code.Logic.Gameplay.Services.Configs.BalanceConfigsProvider
{
    public class GamePushRemoteConfigsProvider : IBalanceConfigsProvider
    {
        private const string PlayerConfigKey = "PlayerConfig";
        private const string AsteroidConfigKey = "AsteroidConfig";
        private const string GunConfigKey = "GunConfig";
        private const string UfoConfigKey = "UfoConfig";
        private const string UfoSpawnerConfigKey = "UfoSpawnerConfig";
        private const string AsteroidSpawnerConfigKey = "AsteroidSpawnerConfig";

        public PlayerConfig PlayerConfig { get; private set; }
        public AsteroidConfig AsteroidConfig { get; private set; }
        public GunConfig GunConfig { get; private set; }
        public UfoConfig UfoConfig { get; private set; }
        public UfoSpawnerConfig UfoSpawnerConfig { get; private set; }
        public AsteroidSpawnerConfig AsteroidSpawnerConfig { get; private set; }

        private readonly ISDKInitializer _sdkInitializer;
        private readonly ILogService _logService;

        private bool _isInitialized;

        public GamePushRemoteConfigsProvider(ISDKInitializer sdkInitializer, ILogService logService)
        {
            _sdkInitializer = sdkInitializer;
            _logService = logService;
        }

        public UniTask Initialize()
        {
            if (_isInitialized)
                return default;

            if (_sdkInitializer.IsGamePushInitialized)
            {
                _isInitialized = true;
                _logService.Log("[Remote Config initialized successfully]", Color.green, true);
            }
            else
            {
                _isInitialized = false;
                _logService.Log("[Remote Config is not initialized, using defaults]", Color.red, true);
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
            AsteroidSpawnerConfig = GetConfig<AsteroidSpawnerConfig>(AsteroidSpawnerConfigKey);
        }

        private T GetConfig<T>(string key) where T : new()
        {
#if UNITY_EDITOR
            return new T();
#else
            return IsCanFetch() && GP_Variables.Has(key) ? 
                JsonUtility.FromJson<T>(GP_Variables.GetString(key))
                : new T();
#endif
        }
    }
}