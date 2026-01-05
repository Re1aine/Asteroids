using System;
using Code.Logic.Gameplay.Services.Configs.Configs.GameAssets;
using Code.Logic.Gameplay.Services.Factories.GameFactory;
using Code.Logic.Gameplay.Services.Observers.UFO;
using R3;
using UnityEngine;

namespace Code.Logic.Gameplay.Entities.Enemy.UFO
{
    public class UFOPresenter
    {
        public event Action<UFOPresenter> Destroyed;
        public UFOView View { get; private set; }
        public UFOModel Model { get; private set; }
        
        private IDamageReceiver _damageReceiver;
        private IDestroyer _destroyer;
        private IUFODeathObserver _ufoDeathObserver;
        private IGameFactory _gameFactory;
        
        private readonly CompositeDisposable _disposables = new();
        
        public UFOPresenter(UFOModel model, UFOView view)
        {
            Model = model;
            View = view;

            Model.Config
                .Subscribe(config =>
                    View.Configure(
                        config.Speed))
                .AddTo(_disposables);
        }

        public void Init(IDamageReceiver damageReceiver, IDestroyer destroyer, IUFODeathObserver ufoDeathObserver, IGameFactory gameFactory)
        {
            _damageReceiver = damageReceiver;
            _destroyer = destroyer;
            _ufoDeathObserver = ufoDeathObserver;
            _gameFactory = gameFactory;

            View.Init(damageReceiver);

            View.OnDamageReceived += ReceiveDamage;
            
            _ufoDeathObserver.Start();
        }

        private void ReceiveDamage(DamageType damageType) => 
            _damageReceiver.ReceiverDamage(damageType);

        public void Destroy(DamageType damageType)
        {
            Destroyed?.Invoke(this);

            View.OnDamageReceived -= ReceiveDamage;

            _disposables.Dispose();
            
            _destroyer.Destroy(damageType);

            _gameFactory.CreateVFX(VFXType.UfoDestroyVFX, View.transform.position, Quaternion.identity);
            
            _ufoDeathObserver.Stop();
        }
    }
}