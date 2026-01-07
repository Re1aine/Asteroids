#if UNITY_EDITOR
using Code.Logic.Gameplay.Analytics.AnalyticsKeys;
using Code.Logic.Gameplay.Analytics.AnalyticsStore;
using Code.Logic.Services.SDKInitializer;
using Firebase.Analytics;
using UnityEngine;

namespace Code.Logic.Gameplay.Analytics.FireBase
{
    public class FireBaseAnalytics : IAnalytics
    {
        private readonly ISDKInitializer _sdkInitializer;

        private bool _isInitialized;
        
        public FireBaseAnalytics(ISDKInitializer sdkInitializer)
        {
            _sdkInitializer = sdkInitializer;
        }

        public void Initialize()
        {
            if (_isInitialized)
                return;

            if (_sdkInitializer.IsFireBaseInitialized)
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
        
        private bool IsCanLogEvent()
        {
            if (_isInitialized)
                return true;
            
            Debug.LogWarning("Firebase is not initialized. Event logging skipped.");
            return false;
        }
        
        public void StartSession()
        {
            if(IsCanLogEvent())
                FirebaseAnalytics.LogEvent(AnalyticsEventsName.StartSession);
        }

        public void SendLaserUsedEvent()
        {
            if(IsCanLogEvent())
                FirebaseAnalytics.LogEvent(AnalyticsEventsName.LaserUsed);
        }

        public void EndSession(IAnalyticsStore analyticsStore)
        {
            if (IsCanLogEvent())
                FirebaseAnalytics.LogEvent(AnalyticsEventsName.EndSession,
                    new Parameter(AnalyticsEventParameters.BulletReleaseCount, analyticsStore.BulletReleaseCount),
                    new Parameter(AnalyticsEventParameters.LaserReleaseCount, analyticsStore.LaserReleaseCount),
                    new Parameter(AnalyticsEventParameters.AsteroidKills, analyticsStore.AsteroidKills),
                    new Parameter(AnalyticsEventParameters.UfoKills, analyticsStore.UfoKills));
        }
    }
}
#endif