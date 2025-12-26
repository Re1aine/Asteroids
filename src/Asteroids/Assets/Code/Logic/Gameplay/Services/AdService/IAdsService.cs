using System;
using Code.Logic.Gameplay.Services.AdService.Ad;

namespace Code.Logic.Gameplay.Services.AdService
{
    public interface IAdsService
    {
        event Action<AdContext> AdCompleted; 
        void ShowRewardedAd(AdContext adContext);
        void ShowInterstitialAd(AdContext adContext);
        void Initialize();
    }
}