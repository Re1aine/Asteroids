using System;

public class AsteroidPresenter
{
    public event Action<AsteroidPresenter> Destroyed;
    public AsteroidView View { get; private set; }
    public AsteroidModel Model { get; private set; }
    public IDamageReceiver DamageReceiver { get; private set; }
    
    private IDestroyer _destroyer;
    
    public AsteroidPresenter(AsteroidModel model, AsteroidView view)
    {
        View = view;
        Model = model;
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