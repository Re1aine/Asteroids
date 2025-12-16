using Code.Logic.Gameplay.Analytics.AnalyticsStore;
using Cysharp.Threading.Tasks;

namespace Code.Logic.Gameplay.Analytics
{
    public interface IAnalytics
    {
        UniTask Initialize();
        void StartSession();
        void SendLaserUsedEvent();
        void EndSession(IAnalyticsStore analyticsStore);
    }
}