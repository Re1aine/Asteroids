using System.Threading.Tasks;

public interface IAnalytics
{
    Task InitializeAsync();
    void StartSession();
    void SendLaserUsedEvent();
    void EndSession(IAnalyticsStore analyticsStore);
}