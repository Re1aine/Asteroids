using _Project.Code.Logic.Gameplay.Analytics.AnalyticsStore;

namespace _Project.Code.Logic.Gameplay.Analytics
{
    public interface IAnalytics
    {
        void Initialize();
        void StartSession();
        void SendLaserUsedEvent();
        void EndSession(IAnalyticsStore analyticsStore);
    }
}