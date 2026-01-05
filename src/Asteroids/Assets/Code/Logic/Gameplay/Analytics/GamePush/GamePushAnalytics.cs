using Code.Logic.Gameplay.Analytics.AnalyticsKeys;
using Code.Logic.Gameplay.Analytics.AnalyticsStore;
using Code.Logic.Services.SDKInitializer;
using GamePush;
using UnityEngine;

namespace Code.Logic.Gameplay.Analytics.GamePush
{
    public class GamePushAnalytics : IAnalytics
    {
        private readonly ISDKInitializer _sdkInitializer;
        
        private bool _isInitialized;

        public GamePushAnalytics(ISDKInitializer sdkInitializer)
        {
            _sdkInitializer = sdkInitializer;
        }
        
        public void Initialize()
        {
            if (_isInitialized)
                return;
            
            if (_sdkInitializer.IsGamePushInitialized)
            {
                _isInitialized = true;
                Debug.Log("<b><color=green> [Analytics initialized successfully] </color></b>");
            }
            else
            {
                _isInitialized = false;
                Debug.Log("<b><color=red> [Analytics is not initialized] </color></b>");
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