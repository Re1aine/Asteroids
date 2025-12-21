using System;
using Code.Logic.Gameplay.Services.ConfigsProvider;
using Code.Logic.Gameplay.Services.Observers.Asteroid;
using Code.Logic.Gameplay.Services.Factories.GameFactory;
using UnityEngine;

namespace Code.Logic.Gameplay.Entities.Enemy.Asteroid
{
    public class AsteroidPresenter
    {
        public event Action<AsteroidPresenter> Destroyed;
        public AsteroidView View { get; private set; }
        public AsteroidModel Model { get; private set; }

        private IDamageReceiver _damageReceiver;
        private IDestroyer _destroyer;
        private IAsteroidDeathObserver _asteroidDeathObserver;
        private IGameFactory _gameFactory;

        public AsteroidPresenter(AsteroidModel model, AsteroidView view)
        {
            View = view;
            Model = model;
        }

        public void Init(IDamageReceiver damageReceiver, IDestroyer destroyer, IAsteroidDeathObserver asteroidDeathObserver, IGameFactory gameFactory)
        {
            _damageReceiver = damageReceiver;
            _destroyer = destroyer;
            _asteroidDeathObserver = asteroidDeathObserver;
            _gameFactory =  gameFactory;

            View.Init(damageReceiver);

            View.OnDamageReceived += ReceiveDamage;

            _asteroidDeathObserver.Start();
        }

        private void ReceiveDamage(DamageType damageType) => 
            _damageReceiver.ReceiverDamage(damageType);

        public void Destroy(DamageType damageType)
        {
            Destroyed?.Invoke(this);

            View.OnDamageReceived -= ReceiveDamage;
            
            _destroyer.Destroy(damageType);

            _gameFactory.CreateVFX(VFXType.AsteroidDestroyVFX, View.transform.position, Quaternion.identity);
            
            _asteroidDeathObserver.Stop();
        }
    }
}