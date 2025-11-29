using System;
using System.Threading.Tasks;
using Code.Logic.Gameplay.Analytics;
using Code.Logic.Gameplay.Analytics.AnalyticsKeys;
using Code.Logic.Gameplay.Analytics.AnalyticsStore;
using GamePush;
using UnityEngine;

public class GamePushAnalytics : IAnalytics
{
    private bool _isInitialized;

    public async Task InitializeAsync()
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
        GP_Analytics.Goal(AnalyticsEventsName.StartSession, 0);
    }

    public void SendLaserUsedEvent()
    {
        GP_Analytics.Goal(AnalyticsEventsName.LaserUsed, AnalyticsEventParameters.LaserReleaseCount);
    }

    public void EndSession(IAnalyticsStore analyticsStore)
    {
        GP_Analytics.Goal(AnalyticsEventsName.EndSession, 0);
    }
}