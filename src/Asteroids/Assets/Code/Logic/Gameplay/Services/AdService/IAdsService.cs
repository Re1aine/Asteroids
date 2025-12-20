using System;

public interface IAdsService
{
    event Action<AdContext> AdCompleted; 
    void ShowRewardedAd(AdContext adContext);
    void ShowInterstitialAd(AdContext adContext);
    void Initialize();
}