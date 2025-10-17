using Code.Logic.Gameplay.Services.Providers.PlayerProvider;
using R3;
using UnityEngine;

namespace Code.UI.PlayerStatsWindow
{
    public class PlayerStatsWindowPresenter
    {
        public PlayerStatsWindowModel Model {get; private set;}
        public PlayerStatsWindowView View {get; private set;}
    
        private IPlayerProvider _playerProvider;

        private readonly CompositeDisposable _disposables = new();
        
        public PlayerStatsWindowPresenter(PlayerStatsWindowModel model, PlayerStatsWindowView view)
        {
            Model = model;
            View = view;
        }
    
        public void Init(IPlayerProvider playerProvider)
        {
            _playerProvider = playerProvider;
            
            _playerProvider.Player.View.Position
                .Subscribe(position => Model.SetPosition(position))
                .AddTo(_disposables);
            
            _playerProvider.Player.View.Rotation
                .Subscribe(rotation => Model.SetRotation(rotation))
                .AddTo(_disposables);
            
            _playerProvider.Player.View.Velocity
                .Subscribe(velocity => Model.SetVelocity(velocity))
                .AddTo(_disposables);
            
            _playerProvider.Player.View.Gun.LaserCharges
                .Subscribe(charges => Model.SetLaserCharges(charges))
                .AddTo(_disposables);
            
            _playerProvider.Player.View.Gun.CooldownLaser
                .Subscribe(cooldown => Model.SetLaserCooldown(cooldown))
                .AddTo(_disposables);
            
            Model.Position
                .Subscribe(position => View.SetPosition(position))
                .AddTo(_disposables);
            
            Model.Rotation
                .Subscribe(rotation => View.SetRotation(rotation))
                .AddTo(_disposables);
            
            Model.Velocity
                .Subscribe(velocity => View.SetVelocity(velocity))
                .AddTo(_disposables);
            
            Model.LaserCharges
                .Subscribe(charges  => View.SetLaserCharges(charges))
                .AddTo(_disposables);
            
            Model.LaserCooldown
                .Subscribe(cooldown => View.SetLaserCooldown(cooldown))
                .AddTo(_disposables);
        }

        public void Destroy()
        { 
            _disposables.Dispose();
            View.Destroy();
        }
    }
}