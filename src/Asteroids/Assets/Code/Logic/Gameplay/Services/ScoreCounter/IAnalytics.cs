public interface IAnalytics
{
    void StartSession();
    void SendLaserUsedEvent();
    void EndSession();
}