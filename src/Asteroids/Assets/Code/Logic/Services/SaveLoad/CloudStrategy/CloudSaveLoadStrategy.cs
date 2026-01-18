using System;
using Code.Infrastructure.Common.LogService;
using Code.Logic.Services.Repository.Player;
using Code.Logic.Services.SDKInitializer;
using Cysharp.Threading.Tasks;
using GamePush;
using R3;
using UnityEngine;

namespace Code.Logic.Services.SaveLoad.CloudStrategy
{
    public class CloudSaveLoadStrategy : ICloudSaveLoadStrategy, IDisposable
    {
        private const string PlayerSaveDataKey = "PlayerSaveData";

        public Observable<PlayerSaveData> SyncFallBack => _syncFallBack;
        private readonly Subject<PlayerSaveData> _syncFallBack = new();

        private readonly ISDKInitializer _sdkInitializer;
        private readonly ILogService _logService;

        private PlayerSaveData _currentSaveData;

        public CloudSaveLoadStrategy(ISDKInitializer sdkInitializer, ILogService logService)
        {
            _sdkInitializer = sdkInitializer;
            _logService = logService;

            GP_Player.OnSyncComplete += OnSyncComplete;
            GP_Player.OnSyncError += OnSyncError;
        }
    
        public UniTask SetPlayerData(PlayerSaveData data)
        {
            _currentSaveData = data;
        
            GP_Player.Set(PlayerSaveDataKey, JsonUtility.ToJson(data));
            GP_Player.Sync();
        
            return default;
        }

        public async UniTask<PlayerSaveData> GetPlayerData()
        {
            if(!await IsAvailable())
                return null;
        
            return await UniTask.FromResult(
                JsonUtility.FromJson<PlayerSaveData>(GP_Player.GetString(PlayerSaveDataKey)));
        }
    
        public async UniTask<bool> IsAvailable()
        {
#if UNITY_EDITOR
            return await UniTask.FromResult(false);
#else
        return await UniTask.FromResult(InternetHelper.HasInternet());
#endif
        }
    
        private void OnSyncComplete() => 
            _logService.LogInfo("Your saves sync on cloud");

        private void OnSyncError()
        {
            _logService.LogError("Cannot sync your saves on cloud");
            _syncFallBack.OnNext(_currentSaveData);
        }
    
        public void Dispose()
        {
            GP_Player.OnSyncComplete -= OnSyncComplete;
            GP_Player.OnSyncError -= OnSyncError;
        }
    }
}