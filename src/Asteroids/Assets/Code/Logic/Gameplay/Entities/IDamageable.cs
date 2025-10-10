using Code.Logic.Gameplay.Services;

namespace Code.Logic.Gameplay.Entities
{
    public interface IDamageable
    {
        IDamageReceiver DamageReceiver { get; }

        void ReceiveDamage(DamageType damageType) => DamageReceiver.ReceiverDamage(damageType);
    }
}