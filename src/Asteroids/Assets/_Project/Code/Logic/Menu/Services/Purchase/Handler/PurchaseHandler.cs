using System;
using _Project.Code.Logic.Gameplay.Services.Holders.RepositoriesHolder;
using _Project.Code.Logic.Menu.Services.Purchase.Catalog;
using _Project.Code.Logic.Menu.Services.Purchase.Product;
using _Project.Code.Logic.Services.Repository.Player;
using GamePush;
using UnityEngine;
using VContainer.Unity;

namespace _Project.Code.Logic.Menu.Services.Purchase.Handler
{
    public class PurchaseHandler : IInitializable, IDisposable
    {
        private readonly IPurchaseService _purchaseService;
        private readonly IPurchaseCatalog _purchaseCatalog;
    
        private readonly PlayerRepository _playerRepository;

        public PurchaseHandler(IPurchaseService purchaseService, IPurchaseCatalog purchaseCatalog, IRepositoriesHolder  repositoriesHolder)
        {
            _purchaseService = purchaseService;
            _purchaseCatalog = purchaseCatalog;
            _playerRepository = repositoriesHolder.GetRepository<PlayerRepository>();
        }

        public void Initialize()
        {
            _purchaseService.OneTimePurchased += OnOneTimePurchase;
            _purchaseService.PermanentPurchased += OnPermanentPurchase;
        }

        private void OnOneTimePurchase(string productId)
        {
            ProductId id = Enum.Parse<ProductId>(productId);
            
            FetchProducts product = _purchaseCatalog.GetProduct(id);
            
            ApplyPurchase(product);

            Debug.Log($"Product purchased: {product.tag}");
            
            _playerRepository.Save();
        }

        private void OnPermanentPurchase(string productId)
        {
            ProductId id = Enum.Parse<ProductId>(productId);
            
            FetchProducts product = _purchaseCatalog.GetProduct(id);

            if (_playerRepository.HasProduct(product))
            {
                Debug.LogWarning($"Product {product.tag} already purchased");
                return;
            }

            _playerRepository.AddPurchasedProduct(product);

            ApplyPurchase(product);

            Debug.Log($"Product purchased: {product.tag}");
            
            _playerRepository.Save();
        }

        private void ApplyPurchase(FetchProducts product)
        {
            switch (product.tag)
            {
                case nameof(ProductId.AdsRemoval): _playerRepository.SetAdsRemoved();
                    break;
            }
        }

        public void Dispose()
        {
            _purchaseService.OneTimePurchased -= OnOneTimePurchase;
            _purchaseService.PermanentPurchased -= OnPermanentPurchase;
        }
    }
}