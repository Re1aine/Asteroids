
namespace Code.Logic.Gameplay.Services.Revive
{
    public class ReviveService : IReviveService
    {
        public bool IsRevived { get; private set; }
        public void Revive() => 
            IsRevived = true;

        public void Reset() => 
            IsRevived = false;
    }
}