using System;
using Code.Logic.Gameplay.Services.AdService.Ad;
using Cysharp.Threading.Tasks;

namespace Code.Logic.Gameplay.Services.AdService
{
    public interface IAdsService
    {
        event Action<AdContext> AdCompleted; 
        bool IsAvailable { get; }
        void ShowRewardedAd(AdContext adContext);
        void ShowInterstitialAd(AdContext adContext);
        void SkipAd(AdContext adContext);
        void Initialize();
    }
}