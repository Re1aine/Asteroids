using System;
using Code.Logic.Menu.Services.Purchase.Produict;
using Code.Logic.Services.SDKInitializer;
using GamePush;
using UnityEngine;

namespace Code.Logic.Menu.Services.Purchase
{
    public class GamePushPurchaseService : IPurchaseService
    {
        public event Action<ProductId> Purchased;
    
        private readonly ISDKInitializer _sdkInitializer;
    
        private bool _isInitialized;

        public GamePushPurchaseService(ISDKInitializer sdkInitializer)
        {
            _sdkInitializer = sdkInitializer;
        }

        public void Initialize()
        {
            if (_isInitialized)
                return;
            
            if (_sdkInitializer.IsGamePushInitialized)
            {
                _isInitialized = true;
                Debug.Log("<b><color=green> [Purchase initialized successfully] </color></b>");
            }
            else
            {
                _isInitialized = false;
                Debug.Log("<b><color=red> [Purchase is not initialized] </color></b>");
            }
        }

        private bool IsCanPurchase()
        {
            if (_isInitialized)
                return true;
        
            //if (_isInitialized && GP_Payments.IsPaymentsAvailable())
            //    return true;
        
            Debug.LogWarning("GamePush is not initialized or not available. Purchase can't proceed.");
            return false;
        }
    
        public void Purchase(ProductId product)
        {
            if (!IsCanPurchase())
                return;
        
            switch (product)
            {
                case ProductId.AdsRemoval: PurchasePermanent(product);
                    break;
            }
        }
    
        private void PurchaseOneTime(ProductId product)
        {
            GP_Payments.Purchase(product.ToString());
            Purchased?.Invoke(product);
        }

        private void PurchasePermanent(ProductId product) => 
            GP_Payments.Purchase(product.ToString(), OnPurchaseSuccess, OnPurchaseFailure);

        private void OnPurchaseSuccess(string productId)
        { 
            ProductId id = Enum.Parse<ProductId>(productId);
        
            Purchased?.Invoke(id);
        }
    
        private void OnPurchaseFailure() => 
            Debug.Log("Purchase operation: FAILURE");
    }
}