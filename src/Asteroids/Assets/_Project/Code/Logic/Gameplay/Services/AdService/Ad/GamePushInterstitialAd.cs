using System;
using GamePush;

namespace Code.Logic.Gameplay.Services.AdService.Ad
{
    public class GamePushInterstitialAd
    {
        public event Action<AdContext> ShowsCompleted;
    
        private AdContext _adContext;

        public void ShowFullscreen(AdContext adContext)
        {
            _adContext = adContext;
        
            GP_Ads.ShowFullscreen(OnFullscreenStart, OnFullscreenClose);
        }

        private void OnFullscreenStart()
        {
        
        }

        private void OnFullscreenClose(bool success) => 
            ShowsCompleted?.Invoke(_adContext);
    }
}