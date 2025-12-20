using System;

namespace Code.Logic.Gameplay.Entities.Enemy.Asteroid
{
    public class AsteroidDamageReceiver :  IDamageReceiver
    {
        public event Action<DamageType> LethalDamageReceived;
        
        private readonly AsteroidPresenter _asteroidPresenter;

        public AsteroidDamageReceiver(AsteroidPresenter presenter) => 
            _asteroidPresenter = presenter;

        public void ReceiverDamage(DamageType damageType)
        {
            LethalDamageReceived?.Invoke(damageType);
            _asteroidPresenter.Destroy(damageType);
        }
    }
}

