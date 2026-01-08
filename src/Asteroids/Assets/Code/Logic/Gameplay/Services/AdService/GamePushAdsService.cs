using System;
using Code.Logic.Gameplay.Services.AdService.Ad;
using Code.Logic.Gameplay.Services.Holders.RepositoriesHolder;
using Code.Logic.Menu.Services.Purchase.Product;
using Code.Logic.Services.Repository.Player;
using Code.Logic.Services.SDKInitializer;
using GamePush;
using UnityEngine;

namespace Code.Logic.Gameplay.Services.AdService
{
    public class GamePushAdsService : IAdsService, IDisposable
    {
        public event Action<AdContext> AdCompleted;

        private GamePushInterstitialAd _interstitialAd;
        private GamePushRewardedAd _rewardedAd;

        private readonly ISDKInitializer _sdkInitializer;
        
        private readonly PlayerRepository _playerRepository;

        private bool _isInitialized;
    
        public GamePushAdsService(ISDKInitializer sdkInitializer, IRepositoriesHolder repositoriesHolder)
        {
            _sdkInitializer = sdkInitializer;
            _playerRepository = repositoriesHolder.GetRepository<PlayerRepository>();
        }

        public void Initialize()
        {
            if (_isInitialized)
                return;

            if (_sdkInitializer.IsGamePushInitialized)
            {
                _isInitialized = true;
                Debug.Log("<b><color=green> [AdsService initialized successfully] </color></b>");
                LoadAds();
            }
            else
            {
                _isInitialized = false;
                Debug.Log("<b><color=red> [AdsService is not initialized] </color></b>");
            }
        }

        public void ShowRewardedAd(AdContext adContext)
        {
            if (!IsCanShow(adContext))
                return;
            
            if(GP_Ads.IsRewardedAvailable())
                _rewardedAd.ShowRewarded(adContext);
        }

        public void ShowInterstitialAd(AdContext adContext)
        {
            if (!IsCanShow(adContext))
                return;
        
            if(GP_Ads.IsFullscreenAvailable())
                _interstitialAd.ShowFullscreen(adContext);
        }

        private bool IsCanShow(AdContext adContext)
        {
            if (_isInitialized && !_playerRepository.IsAdsRemoved.CurrentValue)
                return true;

            if (_playerRepository.IsAdsRemoved.CurrentValue)
            {
                Debug.Log($"<color=yellow><b>Player has [{nameof(ProductId.AdsRemoval)}] purchase. Ads skipped.");
                SkipAd(adContext);
                return false;
            }
            
            Debug.LogWarning("GamePush is not initialized. Ads skipped.");
            SkipAd(adContext);
        
            return false;
        }

        private void SkipAd(AdContext adContext) => 
            AdCompleted?.Invoke(adContext);

        private void LoadAds()
        {
            _interstitialAd = new GamePushInterstitialAd();
            _rewardedAd = new GamePushRewardedAd();
        
            _interstitialAd.ShowsCompleted += AdCompleted;
            _rewardedAd.ShowsCompleted += AdCompleted;
        }
    
        public void Dispose()
        {
            if(_interstitialAd != null)
                _interstitialAd.ShowsCompleted -= AdCompleted;
        
            if(_rewardedAd != null)
                _rewardedAd.ShowsCompleted -= AdCompleted;
        }
    }
}