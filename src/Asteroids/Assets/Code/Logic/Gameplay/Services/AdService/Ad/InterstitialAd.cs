using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Code.Logic.Gameplay.Services.AdService.Ad
{
    public class InterstitialAd : IUnityAdsLoadListener, IUnityAdsShowListener
    {
        private const string AndroidAdUnitId = "Interstitial_Android";
        private const string IOSAdUnitId = "Interstitial_iOS";
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

        public void ShowAd(AdContext context)
        {
            Debug.Log("Showing Ad: " + _adUnitId);
        
            _adContext = context;
        
            Advertisement.Show(_adUnitId, this);
        }

        public void OnUnityAdsAdLoaded(string adUnitId) => 
            Debug.Log("Ad Loaded: " + adUnitId);

        public void OnUnityAdsFailedToLoad(string _adUnitId, UnityAdsLoadError error, string message) => 
            Debug.Log($"Error loading Ad Unit: {_adUnitId} - {error.ToString()} - {message}");

        public void OnUnityAdsShowFailure(string _adUnitId, UnityAdsShowError error, string message) => 
            Debug.Log($"Error showing Ad Unit {_adUnitId}: {error.ToString()} - {message}");

        public void OnUnityAdsShowStart(string _adUnitId)
        {
        
        }

        public void OnUnityAdsShowClick(string _adUnitId)
        {
        
        }

        public void OnUnityAdsShowComplete(string _adUnitId, UnityAdsShowCompletionState showCompletionState) => 
            ShowsCompleted?.Invoke(_adContext);
    }
}