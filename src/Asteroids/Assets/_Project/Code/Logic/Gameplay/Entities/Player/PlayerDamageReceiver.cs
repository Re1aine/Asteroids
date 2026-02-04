using System;

namespace _Project.Code.Logic.Gameplay.Entities.Player
{
    public class PlayerDamageReceiver : IDamageReceiver
    {
        public event Action<DamageType> LethalDamageReceived;
        
        private readonly PlayerPresenter _playerPresenter;
        
        public PlayerDamageReceiver(PlayerPresenter presenter) => 
            _playerPresenter = presenter;

        public void ReceiverDamage(DamageType damageType)
        {
            if (damageType == DamageType.Asteroid || 
                damageType == DamageType.AsteroidPart || 
                damageType == DamageType.UFO)
            {
                LethalDamageReceived?.Invoke(damageType);  
            }
        }
    }
}