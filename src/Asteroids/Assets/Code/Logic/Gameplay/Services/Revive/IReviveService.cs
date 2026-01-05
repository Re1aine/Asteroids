namespace Code.Logic.Gameplay.Services.ReviveService
{
    public interface IReviveService
    {
        bool IsRevived { get; }
        void Revive();
        void Reset();
    }
}