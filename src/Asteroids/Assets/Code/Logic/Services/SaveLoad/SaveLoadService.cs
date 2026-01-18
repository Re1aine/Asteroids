using System;
using Code.Logic.Services.Repository.Player;
using Code.Logic.Services.SaveLoad.CloudStrategy;
using Code.Logic.Services.SaveLoad.LocalStrategy;
using Cysharp.Threading.Tasks;
using R3;

namespace Code.Logic.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService, IDisposable
    {
        public Observable<Unit> StrategyChanged => _strategyChanged;
        private readonly Subject<Unit> _strategyChanged = new();
        public bool HasConflict { get; private set; }
        public ISaveLoadStrategy Current { get; private set; }
        public ReadOnlyReactiveProperty<bool> IsAutoMode => _isAutoMode;
        private readonly ReactiveProperty<bool> _isAutoMode = new();

        private readonly ILocalSaveLoadStrategy _local;
        private readonly ICloudSaveLoadStrategy _cloud;

        private PlayerSaveData _localData;
        private PlayerSaveData _cloudData; 
    
        private readonly CompositeDisposable _disposables = new();

        public SaveLoadService(ILocalSaveLoadStrategy local, ICloudSaveLoadStrategy cloud)
        {
            _local = local;
            _cloud = cloud;

            _cloud.SyncFallBack
                .Subscribe(OnCloudFallBack)
                .AddTo(_disposables);
        }

        public async UniTask Preload()
        {
            _local.Initialize();
        
            _localData = await _local.GetPlayerData();
            _cloudData = await _cloud.GetPlayerData();

            HasConflict = _cloudData != null && DateTime.Compare(
                    DateTime.TryParse(_localData.LastSavedTime, out var l) ? l : DateTime.MinValue,
                    DateTime.TryParse(_cloudData.LastSavedTime, out var c) ? c : DateTime.MinValue) 
                > 0;
        }

        public void SetPlayerData(PlayerSaveData data) => 
            Current.SetPlayerData(data);

        public async UniTask<PlayerSaveData> GetPlayerData() => 
            await Current.GetPlayerData();

        public void UseLocal()
        {
            Current = _local;
            _strategyChanged?.OnNext(Unit.Default);
        }

        public void UseCloud()
        {
            Current = _cloud;
            _strategyChanged?.OnNext(Unit.Default);
        }

        public async UniTask ResolveWithLocal()
        {
            var data = await _local.GetPlayerData();

            await _local.SetPlayerData(data);
            await _cloud.SetPlayerData(data);
        
            UseLocal();

            SetAutoMode(false);
        }

        public async UniTask ResolveWithCloud()
        {
            var data = await _cloud.GetPlayerData();

            await _local.SetPlayerData(data);
            await _cloud.SetPlayerData(data);
        
            UseCloud();

            SetAutoMode(false);
        }

        public void ResolveAutomatically()
        {
            SetAutoMode(true);
        
            if (_cloudData == null)
            {
                UseLocal();
                return;
            }
        
            UseCloud();
        }

        public void SetAutoMode(bool isActive) => 
            _isAutoMode.Value =  isActive;
    
        private void OnCloudFallBack(PlayerSaveData data)
        {
            _local.SetPlayerData(data);

            UseLocal();

            SetAutoMode(true);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}