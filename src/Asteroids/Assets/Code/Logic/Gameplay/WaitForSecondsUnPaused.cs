using UnityEngine;

public class WaitForSecondsUnPaused : CustomYieldInstruction
{
    private readonly IPauseService _pauseService;
    private float _seconds;

    public WaitForSecondsUnPaused(IPauseService pauseService, float seconds)
    {
        _pauseService = pauseService;
        _seconds = seconds;
    }
    
    public override bool keepWaiting
    {
        get
        {
            if (_pauseService.IsPaused)
                return true;
            
            _seconds -= Time.deltaTime;
            return _seconds > 0f;
        }
    }
}