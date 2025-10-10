using System;
using Code.Logic.Gameplay.Services;

namespace Code.Logic.Gameplay.Entities.Enemy.UFO
{
    public class UFOPresenter
    {
        public event Action<UFOPresenter> Destroyed;
        public UFOView View { get; private set; }
        public UFOModel Model { get; private set; }
        public IDamageReceiver  DamageReceiver { get; private set; }
    
        private IDestroyer _destroyer;

        public UFOPresenter(UFOModel model, UFOView view)
        {
            Model = model;
            View = view;
        }

        public void Init(IDamageReceiver damageReceiver, IDestroyer destroyer)
        {
            DamageReceiver = damageReceiver;
            _destroyer = destroyer;

            View.Init(damageReceiver);
        
            View.OnDamageReceived += ReceiveDamage;
        }

        public void ReceiveDamage(DamageType damageType) => 
            DamageReceiver.ReceiverDamage(damageType);

        public void Destroy(DamageType damageType)
        {
            Destroyed?.Invoke(this);
        
            View.OnDamageReceived -= ReceiveDamage;
        
            _destroyer.Destroy(damageType);
        }
    }
}