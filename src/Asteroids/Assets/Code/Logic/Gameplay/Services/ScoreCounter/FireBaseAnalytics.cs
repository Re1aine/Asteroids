using System;
using System.Threading.Tasks;
using Firebase;
using Firebase.Analytics;
using UnityEngine;

    public class FireBaseAnalytics : IAnalytics
    {
        private FirebaseApp _firebaseApp;
        
        private bool _isInitialized;
        
        public async Task InitializeAsync()
        {
            if (_isInitialized)
                return;
            
            try
            {
                var dependencyStatus = await FirebaseApp.CheckAndFixDependenciesAsync();
                if (dependencyStatus != DependencyStatus.Available)
                {
                    Debug.LogError($"Failed to initialize Firebase: {dependencyStatus}");
                    return;
                }
       
                _firebaseApp = FirebaseApp.DefaultInstance;
                _isInitialized = true;
                
                Debug.Log("<b><color=green> Firebase initialized successfully! </color></b>");
            }
            catch (Exception e)
            {
                _isInitialized = false;
                Debug.LogException(e);
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