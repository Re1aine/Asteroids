using Code.Logic.Gameplay.Entities;

namespace Code.Logic.Gameplay.Services.DeathProcessor
{
    public interface IPlayerDeathProcessor
    {
        void StartProcess(DamageType damageType);
        void CancelProcess();
        void CompleteProcess();
    }
}