using System;

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
    }

    public void ReceiveDamage(DamageType damageType) => 
        _damageReceiver.ReceiverDamage(damageType);

    public void DecrementHealth() => 
        Model.DecrementHealth();

    public void Destroy(DamageType  damageType)
    {
        Destroyed?.Invoke();
        _destroyer.Destroy(damageType);
    }
}