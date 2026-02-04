using _Project.Code.Logic.Gameplay.Entities;

namespace _Project.Code.Logic.Gameplay.Services.Death.PlayerDeathProcessor
{
    public interface IPlayerDeathProcessor
    {
        void StartProcess(DamageType damageType);
        void CancelProcess();
        void CompleteProcess();
    }
}