using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAdsService : IAdsService, IUnityAdsInitializationListener
{
    private const string AndroidGameId = "";
    private const string IOSGameId = "";

    private readonly bool _testMode = true;
    private string _gameId;
    
    private InterstitialAd _interstitialAd;

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
        
        _interstitialAd = new InterstitialAd();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"<color=red>Unity Ads Initialization Failed: {error.ToString()} - {message}<color=red>");
    }

    public void ShowRewardedAd()
    {
        _interstitialAd.LoadAd();
    }

    public void ShowInterstitialAd()
    {
    }
}

public interface IAdsService
{
    void ShowRewardedAd();
    void ShowInterstitialAd();
    void Initialize();
}