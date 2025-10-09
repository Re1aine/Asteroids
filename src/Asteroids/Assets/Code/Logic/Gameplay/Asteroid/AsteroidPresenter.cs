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

    public void Init(IDamageReceiver receiver, IDestroyer destroyer)
    {
        DamageReceiver = receiver;
        _destroyer = destroyer;
    }

    public void ReceiveDamage(DamageType damageType) => 
        DamageReceiver.ReceiverDamage(damageType);

    public void Destroy(DamageType damageType)
    {
        Destroyed?.Invoke(this);
        _destroyer.Destroy(damageType);
    }
}