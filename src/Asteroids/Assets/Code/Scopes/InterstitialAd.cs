using UnityEngine;
using UnityEngine.Advertisements;
using VContainer;

public class InterstitialAd : IUnityAdsLoadListener, IUnityAdsShowListener
{
    private const string AndroidAdUnitId = "Interstitial_Android";
    private const string IOSAdUnitId = "Interstitial_iOS";
    
    private string _adUnitId;
    
    private IAdsService _adsService;

    [Inject]
    public void Construct(IAdsService adsService)
    {
        _adsService =  adsService;
        
        _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? IOSAdUnitId
            : AndroidAdUnitId;
    }
    
    public void LoadAd()
    {
        Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }

    private void ShowAd()
    {
        Debug.Log("Showing Ad: " + _adUnitId);
        Advertisement.Show(_adUnitId, this);
    }

    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        ShowAd();
    }

    public void OnUnityAdsFailedToLoad(string _adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {_adUnitId} - {error.ToString()} - {message}");
        
    }

    public void OnUnityAdsShowFailure(string _adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {_adUnitId}: {error.ToString()} - {message}");
        
    }

    public void OnUnityAdsShowStart(string _adUnitId)
    {
        
    }

    public void OnUnityAdsShowClick(string _adUnitId)
    {
        
    }

    public void OnUnityAdsShowComplete(string _adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        
    }
}