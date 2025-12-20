public interface IReviveService
{
    bool IsRevived { get; }
    void Revive();
    void Reset();
}