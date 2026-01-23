using System;
using GamePush;

namespace _Project.Code.Logic.Gameplay.Services.AdService.Ad
{
    public class GamePushRewardedAd
    {
        public event Action<AdContext> ShowsCompleted;

        private AdContext _adContext;
        public void ShowRewarded(AdContext adContext)
        {
            _adContext = adContext;
        
            GP_Ads.ShowRewarded("", OnGetReward, OnRewardedStart, OnRewardedClose);
        }

        private void OnRewardedStart()
        {
        
        }

        private void OnGetReward(string value) => 
            ShowsCompleted?.Invoke(_adContext);

        private void OnRewardedClose(bool success)
        {
       
        }
    }
}