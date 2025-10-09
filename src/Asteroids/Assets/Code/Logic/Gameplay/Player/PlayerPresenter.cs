using System;
using Code.Logic.Gameplay.Services;

namespace Code.Logic.Gameplay.Player
{
    public class PlayerPresenter
    {
        public event Action Destroyed;
    
        private IDamageReceiver _damageReceiver;
        private IDestroyer _destroyer;
    
        public PlayerModel Model {get ; private set; }
        public PlayerView View  {get; private set; }
    
        public PlayerPresenter(PlayerModel model, PlayerView view)
        {
            Model = model;
            View = view;
        }

        public void Init(IDamageReceiver damageReceiver, IDestroyer destroyer)
        {
            _damageReceiver = damageReceiver;
            _destroyer = destroyer;

            View.OnDamageReceived += ReceiveDamage;
        }

        public void ReceiveDamage(DamageType damageType) => 
            _damageReceiver.ReceiverDamage(damageType);

        public void DecrementHealth() => 
            Model.DecrementHealth();

        public void Destroy(DamageType  damageType)
        {
            Destroyed?.Invoke();
        
            View.OnDamageReceived -= ReceiveDamage;
        
            _destroyer.Destroy(damageType);
        }
    }
}