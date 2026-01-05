namespace Code.Logic.Gameplay.Services.Revive
{
    public interface IReviveService
    {
        bool IsRevived { get; }
        void Revive();
        void Reset();
    }
}