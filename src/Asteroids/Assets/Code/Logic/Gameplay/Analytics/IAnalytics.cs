using System.Threading.Tasks;
using Code.Logic.Gameplay.Analytics.AnalyticsStore;

namespace Code.Logic.Gameplay.Analytics
{
    public interface IAnalytics
    {
        Task InitializeAsync();
        void StartSession();
        void SendLaserUsedEvent();
        void EndSession(IAnalyticsStore analyticsStore);
    }
}