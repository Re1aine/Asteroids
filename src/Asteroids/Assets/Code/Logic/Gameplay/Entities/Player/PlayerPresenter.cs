using R3;

namespace Code.Logic.Gameplay.Entities.Player
{
    public class PlayerPresenter
    {
        public IDamageReceiver DamageReceiver { get; private set; }
        private IDestroyer _destroyer;

        private readonly CompositeDisposable _disposables = new();
        
        public PlayerModel Model {get ; private set; }
        public PlayerView View  {get; private set; }
    
        public PlayerPresenter(PlayerModel model, PlayerView view)
        {
            Model = model;
            View = view;
            
            Model.PlayerConfig
                .Subscribe(config => 
                    View.Configure(
                        config.DecelerationMove,
                        config.AccelerationMove,
                        config.MoveSpeed,
                        config.RotateSpeed)
                ).AddTo(_disposables);
            
            Model.GunConfig
                .Subscribe(config => 
                    View.Gun.Configure(
                        config.BulletCooldown,
                        config.LaserShootCooldown,
                        config.LaserShootTime,
                        config.LaserRange,
                        config.LaserChargesMax,
                        config.LaserChargesCurrent,
                        config.LaserChargeRefillCooldown)
                ).AddTo(_disposables);
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
            
            _disposables.Dispose();
            
            _destroyer.Destroy(damageType);
        }
    }
}