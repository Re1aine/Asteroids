
public class PauseService : IPauseService
{
    public bool IsPaused => _isPaused;

    private bool _isPaused;

    public void Pause() => 
        _isPaused = true;

    public void UnPause() => 
        _isPaused = false;
}