
using System;
using UnityEngine;

public class UFOPresenter : PresenterBase
{
    public event Action<UFOPresenter> Destroyed;
    public UFOView View { get; private set; }
    public UFOModel Model { get; private set; }
    public IDamageReceiver  DamageReceiver { get; private set; }
    
    private IDestroyer _destroyer;

    public UFOPresenter(UFOModel model, UFOView view) : base(model, view)
    {
        Model = model;
        View = view;
    }

    public void Init(IDamageReceiver damageReceiver, IDestroyer destroyer)
    {
        DamageReceiver = damageReceiver;
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