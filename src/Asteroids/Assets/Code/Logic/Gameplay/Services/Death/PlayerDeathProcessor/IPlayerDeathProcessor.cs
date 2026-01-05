using Code.Logic.Gameplay.Entities;

namespace Code.Logic.Gameplay.Services.Death.PlayerDeathProcessor
{
    public interface IPlayerDeathProcessor
    {
        void StartProcess(DamageType damageType);
        void CancelProcess();
        void CompleteProcess();
    }
}