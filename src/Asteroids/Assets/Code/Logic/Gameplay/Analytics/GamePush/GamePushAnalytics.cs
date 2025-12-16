using System;
using Code.Logic.Gameplay.Analytics.AnalyticsKeys;
using Code.Logic.Gameplay.Analytics.AnalyticsStore;
using Cysharp.Threading.Tasks;
using GamePush;
using UnityEngine;

namespace Code.Logic.Gameplay.Analytics.GamePush
{
    public class GamePushAnalytics : IAnalytics
    {
        private bool _isInitialized;
    
        public async UniTask Initialize()
        {
            if (_isInitialized)
                return;
            
            try
            {
                await GP_Init.Ready;
                _isInitialized = true;
                Debug.Log("<b><color=green> GamePush initialized successfully! </color></b>");
            }
            catch (Exception e)
            {
                _isInitialized = false;
                Debug.LogException(e);
            }
        }

        public void StartSession()
        {
            if(IsCanLogEvent())
                GP_Analytics.Goal(AnalyticsEventsName.StartSession, 0);
        }

        private bool IsCanLogEvent()
        {
            if (_isInitialized)
                return true;
            
            Debug.LogWarning("GamePush is not initialized. Event logging skipped.");
            return false;
        }

        public void SendLaserUsedEvent()
        {
            if(IsCanLogEvent())
                GP_Analytics.Goal(AnalyticsEventsName.LaserUsed, AnalyticsEventParameters.LaserReleaseCount);
        }

        public void EndSession(IAnalyticsStore analyticsStore)
        {
            if(IsCanLogEvent())
                GP_Analytics.Goal(AnalyticsEventsName.EndSession, 0);
        }
    }
}