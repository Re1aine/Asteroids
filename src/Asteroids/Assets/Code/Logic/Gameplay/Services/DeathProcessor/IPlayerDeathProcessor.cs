using Code.Logic.Gameplay.Entities;

public interface IPlayerDeathProcessor
{
    void StartProcess(DamageType damageType);
    void CancelProcess();
    void CompleteProcess();
}