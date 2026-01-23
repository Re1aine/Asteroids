using System;
using System.Collections.Generic;
using Code.Logic.Menu.Services.Purchase.Product;
using Code.Logic.Services.SaveLoad;
using Cysharp.Threading.Tasks;
using GamePush;
using R3;
using VContainer.Unity;

namespace Code.Logic.Services.Repository.Player
{
    public class PlayerRepository : IRepository, IInitializable, IDisposable
    {
        private readonly ISaveLoadService _saveLoadService;

        public ReadOnlyReactiveProperty<int> HighScore => _highScore;
        private readonly ReactiveProperty<int> _highScore = new();

        public ReadOnlyReactiveProperty<bool> IsAdsRemoved => _isAdsRemoved;
        private readonly ReactiveProperty<bool> _isAdsRemoved = new();

        public ReadOnlyReactiveProperty<string> LastSaveTime => _lastSaveTime;
        private readonly ReactiveProperty<string> _lastSaveTime = new();

        public ReadOnlyReactiveProperty<List<FetchPlayerPurchases>> PurchasedProducts => _purchasedProducts;
        private readonly ReactiveProperty<List<FetchPlayerPurchases>> _purchasedProducts = new();

        private readonly PlayerSaveData _playerSaveData = new();
        
        private readonly CompositeDisposable _disposables = new();
        
        public PlayerRepository(ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
        }

        public void Initialize() => 
            SetupSubscribes();

        public async UniTask Load()
        {
            PlayerSaveData data = await _saveLoadService.GetPlayerData();
            
            _highScore.Value = data.HighScore;
            _isAdsRemoved.Value = data.IsAdsRemoved;
            _purchasedProducts.Value = data.PurchasedProducts;
            _lastSaveTime.Value = data.LastSavedTime;
        }

        public void Delete()
        {
            _highScore.Value = 0;
            _isAdsRemoved.Value = false;
            _purchasedProducts.Value.Clear();
            _lastSaveTime.Value = "1970-01-01T00:00:00Z";
            
            _saveLoadService.SetPlayerData(_playerSaveData);
        }

        public void Update()
        {
            
        }

        public void Save()
        {
            SetLastSavedTime(DateTime.UtcNow.ToString("o"));            
            _saveLoadService.SetPlayerData(_playerSaveData);
        }

        public void SetHighScore(int value) => 
            _highScore.Value = value;

        public void SetAdsRemoved() => 
            _isAdsRemoved.Value = true;

        private void SetLastSavedTime(string value) => 
            _lastSaveTime.Value = value;

        public void AddPurchasedProduct(FetchProducts product)
        {
            _purchasedProducts.Value.Add(
                new PlayerPurchaseProductWrapper(product)
                    .Wrap());
        }

        public bool HasProduct(FetchProducts product) => 
            _purchasedProducts.Value.Exists(p => p.productId == product.id);

        private void SetupSubscribes()
        {
            HighScore
                .Subscribe(highScore => _playerSaveData.HighScore = highScore)
                .AddTo(_disposables);
            
            IsAdsRemoved
                .Subscribe(isAdsRemoved => _playerSaveData.IsAdsRemoved = isAdsRemoved)
                .AddTo(_disposables);

            PurchasedProducts
                .Subscribe(products => _playerSaveData.PurchasedProducts = products)
                .AddTo(_disposables);
            
            LastSaveTime
                .Subscribe(lastSaveTime => _playerSaveData.LastSavedTime = lastSaveTime)
                .AddTo(_disposables);
            
            _saveLoadService.StrategyChanged
                .Subscribe(x => _ = Load())
                .AddTo(_disposables);
        }

        public void Dispose() => 
            _disposables.Dispose();
    }
}