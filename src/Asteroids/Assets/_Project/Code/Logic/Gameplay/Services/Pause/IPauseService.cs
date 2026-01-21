namespace Code.Logic.Gameplay.Services.Pause
{
    public interface IPauseService
    {
        bool IsPaused { get; }
        void Pause();
        void UnPause();
    }
}