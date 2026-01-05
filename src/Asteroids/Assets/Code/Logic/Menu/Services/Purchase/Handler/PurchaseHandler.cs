using System;
using Code.Logic.Gameplay.Services.Holders.RepositoriesHolder;
using Code.Logic.Menu.Services.Purchase.Catalog;
using Code.Logic.Menu.Services.Purchase.Produict;
using Code.Logic.Services.Repository.Player;
using UnityEngine;
using VContainer.Unity;

namespace Code.Logic.Menu.Services.Purchase.Handler
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
            _purchaseService.Purchased += OnPurchase;
        }

        private void OnPurchase(ProductId id)
        {
            PurchaseProduct product = _purchaseCatalog.GetProduct(id);

            if (!product.IsOneTime && _playerRepository.HasProduct(product.Id))
            {
                Debug.LogWarning($"Product {product.Id.ToString()} already purchased");
                return;
            }
        
            if(!product.IsOneTime)
                _playerRepository.AddPurchasedProduct(product);
        
            ApplyPurchase(id);

            Debug.Log($"Product purchased: {id.ToString()}");
        
            _playerRepository.Save();
        }

        private void ApplyPurchase(ProductId id)
        {
            switch (id)
            {
                case ProductId.AdsRemoval: _playerRepository.SetAdsRemoved();
                    break;
            }
        }

        public void Dispose()
        {
            _purchaseService.Purchased -= OnPurchase;
        }
    }
}