using System;
using _Project.Code.Logic.Gameplay.Services.AdService.Ad;

namespace _Project.Code.Logic.Gameplay.Services.AdService
{
    public interface IAdsService
    {
        event Action<AdContext> AdCompleted; 
        void ShowRewardedAd(AdContext adContext);
        void ShowInterstitialAd(AdContext adContext);
        void Initialize();
    }
}