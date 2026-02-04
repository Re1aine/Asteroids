using System;

namespace _Project.Code.Logic.Gameplay.Entities
{
    public interface IDamageReceiver
    {
        event Action<DamageType> LethalDamageReceived;
        void ReceiverDamage(DamageType damageType);    
    }
}