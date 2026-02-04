using System;

namespace _Project.Code.Logic.Gameplay.Entities.Enemy.UFO
{
    public class UfoDamageReceiver : IDamageReceiver
    {
        public event Action<DamageType> LethalDamageReceived;
        
        private readonly UFOPresenter _ufoPresenter;

        public UfoDamageReceiver(UFOPresenter presenter) => 
            _ufoPresenter = presenter;


        public void ReceiverDamage(DamageType damageType)
        {
            LethalDamageReceived?.Invoke(damageType);
            _ufoPresenter.Destroy(damageType);
        }
    }
}