namespace Code.Logic.Gameplay.Services.PauseService
{
    public interface IPauseService
    {
        bool IsPaused { get; }
        void Pause();
        void UnPause();
    }
}