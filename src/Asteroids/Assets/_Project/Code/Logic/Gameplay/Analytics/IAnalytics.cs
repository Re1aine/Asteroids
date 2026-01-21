using Code.Logic.Gameplay.Analytics.AnalyticsStore;

namespace Code.Logic.Gameplay.Analytics
{
    public interface IAnalytics
    {
        void Initialize();
        void StartSession();
        void SendLaserUsedEvent();
        void EndSession(IAnalyticsStore analyticsStore);
    }
}