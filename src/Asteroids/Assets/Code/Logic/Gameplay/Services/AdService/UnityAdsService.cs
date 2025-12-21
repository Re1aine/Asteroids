using System;
using Code.Logic.Gameplay.Services.AdService.Ad;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Code.Logic.Gameplay.Services.AdService
{
    public class UnityAdsService : IAdsService, IUnityAdsInitializationListener, IDisposable
    {
        private const string AndroidGameId = "6005982";
        private const string IOSGameId = "6005983";

        public event Action<AdContext> AdCompleted;
    
        private readonly bool _testMode = true;
        private string _gameId;

        private InterstitialAd _interstitialAd;
        private RewardedAds _rewardedAds;
    
        public void Initialize()
        {
            _gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
                ? IOSGameId
                : AndroidGameId;
        
            if (!Advertisement.isInitialized && Advertisement.isSupported) 
                Advertisement.Initialize(_gameId, _testMode, this);
        }

        public void OnInitializationComplete()
        {
            Debug.Log("<color=green>Unity Ads initialization complete<color=green>");
            LoadAds();
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message) => 
            Debug.Log($"<color=red>Unity Ads Initialization Failed: {error.ToString()} - {message}<color=red>");

        private void LoadAds()
        {
            _interstitialAd = new InterstitialAd();
            _rewardedAds =  new RewardedAds();
        
            _interstitialAd.Initialize();
            _rewardedAds.Initialize();
        
            _rewardedAds.LoadAd();
            _interstitialAd.LoadAd();

            _rewardedAds.ShowsCompleted += OnRewardedAdCompleted;
            _interstitialAd.ShowsCompleted += OnInterstitialAdCompleted;
        }

        private void OnInterstitialAdCompleted(AdContext context) => 
            AdCompleted?.Invoke(context);

        private void OnRewardedAdCompleted(AdContext context) => 
            AdCompleted?.Invoke(context);
    
        public void ShowRewardedAd(AdContext adContext) => 
            _rewardedAds.ShowAd(adContext);

        public void ShowInterstitialAd(AdContext adContext) => 
            _interstitialAd.ShowAd(adContext);

        public void Dispose()
        {
            _rewardedAds.ShowsCompleted -= OnRewardedAdCompleted;
            _interstitialAd.ShowsCompleted -= OnInterstitialAdCompleted;
        }
    }
}