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
        public ISaveLoadStrategy CurrentStrategy { get; private set; }
        public ReadOnlyReactiveProperty<bool> IsAutoMode => _isAutoMode;
        private readonly ReactiveProperty<bool> _isAutoMode = new();

        private readonly ILocalSaveLoadStrategy _localStrategy;
        private readonly ICloudSaveLoadStrategy _cloudStrategy;

        private PlayerSaveData _localData;
        private PlayerSaveData _cloudData; 
    
        private readonly CompositeDisposable _disposables = new();

        public SaveLoadService(ILocalSaveLoadStrategy localStrategy, ICloudSaveLoadStrategy cloudStrategy)
        {
            _localStrategy = localStrategy;
            _cloudStrategy = cloudStrategy;

            _cloudStrategy.SyncFallBack
                .Subscribe(OnCloudFallBack)
                .AddTo(_disposables);
        }

        public async UniTask Preload()
        {
            _localStrategy.Initialize();
        
            _localData = await _localStrategy.GetPlayerData();
            _cloudData = await _cloudStrategy.GetPlayerData();

            HasConflict = _cloudData != null && DateTime.Compare(
                    DateTime.TryParse(_localData.LastSavedTime, out var l) ? l : DateTime.MinValue,
                    DateTime.TryParse(_cloudData.LastSavedTime, out var c) ? c : DateTime.MinValue) 
                > 0;
        }

        public void SetPlayerData(PlayerSaveData data) => 
            CurrentStrategy.SetPlayerData(data);

        public async UniTask<PlayerSaveData> GetPlayerData() => 
            await CurrentStrategy.GetPlayerData();

        public void UseLocalStrategy()
        {
            CurrentStrategy = _localStrategy;
            _strategyChanged?.OnNext(Unit.Default);
        }

        public void UseCloudStrategy()
        {
            CurrentStrategy = _cloudStrategy;
            _strategyChanged?.OnNext(Unit.Default);
        }

        public async UniTask ResolveWithLocal()
        {
            var data = await _localStrategy.GetPlayerData();

            await _localStrategy.SetPlayerData(data);
            await _cloudStrategy.SetPlayerData(data);
        
            UseLocalStrategy();

            SetAutoMode(false);
        }

        public async UniTask ResolveWithCloud()
        {
            var data = await _cloudStrategy.GetPlayerData();

            await _localStrategy.SetPlayerData(data);
            await _cloudStrategy.SetPlayerData(data);
        
            UseCloudStrategy();

            SetAutoMode(false);
        }

        public void ResolveAutomatically()
        {
            SetAutoMode(true);
        
            if (_cloudData == null)
            {
                UseLocalStrategy();
                return;
            }
        
            UseCloudStrategy();
        }

        public void SetAutoMode(bool isActive) => 
            _isAutoMode.Value =  isActive;
    
        private void OnCloudFallBack(PlayerSaveData data)
        {
            _localStrategy.SetPlayerData(data);

            UseLocalStrategy();

            SetAutoMode(true);
        }

        public void Dispose() => 
            _disposables.Dispose();
    }
}