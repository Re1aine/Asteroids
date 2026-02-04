using System;
using _Project.Code.Infrastructure.Common.LogService;
using _Project.Code.Logic.Services.Repository.Player;
using _Project.Code.Logic.Services.SDKInitializer;
using _Project.Code.Tools;
using Cysharp.Threading.Tasks;
using GamePush;
using R3;
using UnityEngine;
using UnityEngine.Events;
using VContainer.Unity;

namespace _Project.Code.Logic.Services.SaveLoad.CloudStrategy
{
    public class CloudSaveLoadStrategy : ICloudSaveLoadStrategy, IInitializable, IDisposable
    {
        private const string PlayerSaveDataKey = "PlayerSaveData";

        public Observable<Unit> SyncCompleted => _syncCompleted;
        public Observable<PlayerSaveData> SyncFailed => _syncFailed;

        private readonly Subject<Unit> _syncCompleted = new();
        private readonly Subject<PlayerSaveData> _syncFailed = new();
        
        private readonly ISDKInitializer _sdkInitializer;
        private readonly ILogService _logService;

        private PlayerSaveData _currentSaveData;
        
        private readonly CompositeDisposable _disposables = new();

        public CloudSaveLoadStrategy(ISDKInitializer sdkInitializer, ILogService logService)
        {
            _sdkInitializer = sdkInitializer;
            _logService = logService;
        }

        public void Initialize() => 
            SetupSubscribes();

        public UniTask SetPlayerData(PlayerSaveData data)
        {
            _currentSaveData = data;
        
            GP_Player.Set(PlayerSaveDataKey, JsonUtility.ToJson(data));
            GP_Player.Sync();
        
            return default;
        }

        public async UniTask<PlayerSaveData> GetPlayerData()
        {
            bool isAvailable = await IsAvailable();
#if !UNITY_EDITOR && UNITY_WEBGL
           if(isAvailable)
               return await UniTask.FromResult(JsonUtility.FromJson<PlayerSaveData>(GP_Player.GetString(PlayerSaveDataKey)));
           
           return new PlayerSaveData();
#else
            return await UniTask.FromResult(new PlayerSaveData());
#endif
        }

        private void SetupSubscribes()
        {
            Observable.FromEvent(
                    h => new UnityAction(h),
                    h => GP_Player.OnSyncComplete += h,
                    h => GP_Player.OnSyncComplete -= h
                )
                .Subscribe(_ => OnSyncComplete())
                .AddTo(_disposables);
            
            Observable.FromEvent(
                    h => new UnityAction(h),
                    h => GP_Player.OnSyncError += h,
                    h => GP_Player.OnSyncError -= h
                )
                .Subscribe(_ => OnSyncFailed())
                .AddTo(_disposables);
        }

        private async UniTask<bool> IsAvailable()
        {
            if (!_sdkInitializer.IsGamePushInitialized)
                return false;
            
            return await UniTask.FromResult(InternetHelper.HasInternet());
        }

        private void OnSyncComplete()
        {
            _logService.LogInfo("Your saves sync on cloud");
            _syncCompleted.OnNext(Unit.Default);
        }

        private void OnSyncFailed()
        {
            _logService.LogError("Cannot sync your saves on cloud");
            _syncFailed.OnNext(_currentSaveData);
        }

        public void Dispose() => 
            _disposables?.Dispose();
    }
}