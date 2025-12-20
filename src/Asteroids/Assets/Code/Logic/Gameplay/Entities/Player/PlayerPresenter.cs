namespace Code.Logic.Gameplay.Entities.Player
{
    public class PlayerPresenter
    {
        public IDamageReceiver DamageReceiver { get; private set; }
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
            DamageReceiver = damageReceiver;
            _destroyer = destroyer;
            
            View.OnDamageReceived += ReceiveDamage;
        }

        private void ReceiveDamage(DamageType damageType) => 
            DamageReceiver.ReceiverDamage(damageType);
        
        public void Destroy(DamageType  damageType)
        {
            View.OnDamageReceived -= ReceiveDamage;
            
            _destroyer.Destroy(damageType);
        }
    }
}