using System;
using Code.Logic.Menu.Services.Purchase.Product;
using Code.Logic.Services.SDKInitializer;
using GamePush;
using UnityEngine;

namespace Code.Logic.Menu.Services.Purchase
{
    public class GamePushPurchaseService : IPurchaseService
    {
        public event Action<string> OneTimePurchased;
        public event Action<string> PermanentPurchased;
        
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
#if UNITY_EDITOR            
            if (_isInitialized)
                return true;
#else            
            if (_isInitialized && GP_Payments.IsPaymentsAvailable())
                return true;
#endif

            Debug.LogWarning("GamePush is not initialized or not available. Purchase can't proceed.");
            return false;
        }
    
        public void Purchase(ProductId product)
        {
            if (!IsCanPurchase())
                return;
        
            switch (product)
            {
                case ProductId.AdsRemoval: PurchasePermanent(nameof(ProductId.AdsRemoval));
                    break;
            }
        }

        private void PurchaseOneTime(string id)
        {
            GP_Payments.Purchase(id);
            OneTimePurchased?.Invoke(id);
        }

        private void PurchasePermanent(string id) => 
            GP_Payments.Purchase(id, OnPurchaseSuccess, OnPurchaseFailure);

        private void OnPurchaseSuccess(string id) => 
            PermanentPurchased?.Invoke(id);

        private void OnPurchaseFailure() => 
            Debug.Log("Purchase operation: FAILURE");
    }
}