using System;
using R3;

public class ReviveWindowModel : IDisposable
{
    public ReadOnlyReactiveProperty<bool> IsTimerActive => _isTimerActive;
    public ReadOnlyReactiveProperty<float> TimerDuration => _timerDuration;
    
    private readonly ReactiveProperty<bool> _isTimerActive = new();
    private readonly ReactiveProperty<float> _timerDuration = new();
    
    private readonly IAdsService _adsService;

    public ReviveWindowModel(IAdsService adsService)
    {
        _adsService = adsService;
        
        RunTimer();
    }

    public void Accept()
    {
        SetActiveTimer(false);
        _adsService.ShowRewardedAd(AdContext.DeathRevive);
    }

    public void Decline()
    {
        SetActiveTimer(false);
        _adsService.ShowInterstitialAd(AdContext.DeathInterstitial);
    }

    private void RunTimer()
    {
        SetTimerDuration(5f);
        SetActiveTimer(true);
    }
    
    private void SetActiveTimer(bool isActive) => 
        _isTimerActive.Value = isActive;

    private void SetTimerDuration(float duration) => 
        _timerDuration.Value = duration;

    public void Dispose()
    {
        
    }
}