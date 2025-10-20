
namespace Code.Logic.Gameplay.Entities.Enemy.Asteroid
{
    public class AsteroidDamageReceiver :  IDamageReceiver
    {
        private readonly AsteroidPresenter _asteroidPresenter;
    
        public AsteroidDamageReceiver(AsteroidPresenter presenter)
        {
            _asteroidPresenter = presenter;
        }

        public void ReceiverDamage(DamageType damageType) => 
            _asteroidPresenter.Destroy(damageType);
    }
}

