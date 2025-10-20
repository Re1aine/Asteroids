using System;

public class FireBaseAnalytics : IAnalytics
{
    public event Action GameStarted;
    public event Action LaserUsed;
    public event Action GameEnded;
    
    private readonly IAnalyticsStore _analyticsStore;

    public FireBaseAnalytics(IAnalyticsStore analyticsStore)
    {
        _analyticsStore = analyticsStore;
    }
    
    public void StartSession() => 
        GameStarted?.Invoke();

    public void SendLaserUsedEvent() => 
        LaserUsed?.Invoke();

    public void EndSession() => 
        GameEnded?.Invoke();

    public void Flush() => 
        _analyticsStore.Flush();
}