using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Code.Logic.Gameplay.Services.AdService.Ad
{
    public class RewardedAds : IUnityAdsLoadListener, IUnityAdsShowListener
    {
        private const string AndroidAdUnitId = "Rewarded_Android";
        private const string IOSAdUnitId = "Rewarded_iOS";
        public event Action<AdContext> ShowsCompleted;

        private string _adUnitId;

        private AdContext _adContext;
    
        public void Initialize()
        {
            _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
                ? IOSAdUnitId
                : AndroidAdUnitId;
        }
    
        public void LoadAd()
        {
            Debug.Log("Loading Ad: " + _adUnitId);
            Advertisement.Load(_adUnitId, this);
        }
    
        public void OnUnityAdsAdLoaded(string adUnitId) => 
            Debug.Log("Ad Loaded: " + adUnitId);

        public void ShowAd(AdContext context)
        {
            Debug.Log("Showing Ad: " + _adUnitId);

            _adContext = context;
        
            Advertisement.Show(_adUnitId, this);
        }

        public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
        {
            if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
            {
                Debug.Log("<color=white>Unity Ads Rewarded Ad Completed<color=white>");
                ShowsCompleted?.Invoke(_adContext);
            }
        }

        public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message) => 
            Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");

        public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message) => 
            Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");

        public void OnUnityAdsShowStart(string adUnitId)
        {
        
        }

        public void OnUnityAdsShowClick(string adUnitId)
        {
        
        }
    }
}