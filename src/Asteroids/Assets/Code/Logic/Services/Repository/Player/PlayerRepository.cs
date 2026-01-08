using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Code.Logic.Menu.Services.Purchase.Product;
using Code.Logic.Services.SaveLoad;
using GamePush;
using R3;
using UnityEngine;

namespace Code.Logic.Services.Repository.Player
{
    public class PlayerRepository : IRepository, IDisposable
    {
        private readonly ISaveLoadService _saveLoadService;
        
        private readonly PlayerSaveData _playerSaveData = new();

        public ReadOnlyReactiveProperty<int> HighScore => _highScore;
        private readonly ReactiveProperty<int> _highScore = new();

        public ReadOnlyReactiveProperty<bool> IsAdsRemoved => _isAdsRemoved;
        private readonly ReactiveProperty<bool> _isAdsRemoved = new();

        public ReadOnlyReactiveProperty<List<FetchPlayerPurchases>> PurchaseProduct => _purchasedProducts;
        private readonly ReactiveProperty<List<FetchPlayerPurchases>> _purchasedProducts = new();
        
        private readonly CompositeDisposable _disposables = new();
        
        public PlayerRepository(ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
            
            HighScore
                .Subscribe(highScore => _playerSaveData.HighScore = highScore)
                .AddTo(_disposables);
            
            IsAdsRemoved
                .Subscribe(isAdsRemoved => _playerSaveData.IsAdsRemoved = isAdsRemoved)
                .AddTo(_disposables);

            PurchaseProduct
                .Subscribe(products => _playerSaveData.PurchasedProducts = products)
                .AddTo(_disposables);
        }
        
        public void Load()
        {
            _highScore.Value = _saveLoadService.GetPlayerData().HighScore;
            _isAdsRemoved.Value = _saveLoadService.GetPlayerData().IsAdsRemoved;
            _purchasedProducts.Value = _saveLoadService.GetPlayerData().PurchasedProducts;
        }

        public void Delete()
        {
            _saveLoadService.SetPlayerData(_playerSaveData);
            
            _highScore.Value = 0;
            _isAdsRemoved.Value = false;
            _purchasedProducts.Value.Clear();
        }

        public void Update()
        {
            
        }

        public void Save() => 
            _saveLoadService.SetPlayerData(_playerSaveData);

        public void SetHighScore(int value) => 
            _highScore.Value = value;

        public void SetAdsRemoved() => 
            _isAdsRemoved.Value = true;

        public void AddPurchasedProduct(FetchProducts product)
        {
            _purchasedProducts.Value.Add(
                new PlayerPurchaseProductWrapper(product)
                    .Wrap());
        }

        public bool HasProduct(FetchProducts product) => 
            _purchasedProducts.Value.Exists(p => p.productId == product.id);

        public void Dispose() => 
            _disposables.Dispose();
    }
}