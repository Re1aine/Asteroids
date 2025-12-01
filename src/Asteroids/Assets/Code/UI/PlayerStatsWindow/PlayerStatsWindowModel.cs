using System;
using Code.Logic.Gameplay.Services.Providers.PlayerProvider;
using R3;
using UnityEngine;

namespace Code.UI.PlayerStatsWindow
{
    public class PlayerStatsWindowModel : IDisposable
    {
        public R3.ReadOnlyReactiveProperty<Vector3> Position => _position;
        public R3.ReadOnlyReactiveProperty<Quaternion> Rotation => _rotation;
        public R3.ReadOnlyReactiveProperty<float> Velocity => _velocity;
        public R3.ReadOnlyReactiveProperty<int> LaserCharges => _laserCharges;
        public R3.ReadOnlyReactiveProperty<float> LaserCooldown => _laserCooldown;
        
        private readonly R3.ReactiveProperty<Vector3> _position = new();
        private readonly R3.ReactiveProperty<Quaternion> _rotation = new();
        private readonly R3.ReactiveProperty<float> _velocity = new();
        private readonly R3.ReactiveProperty<int> _laserCharges = new();
        private readonly R3.ReactiveProperty<float> _laserCooldown = new();
        
        private readonly CompositeDisposable _disposables = new();

        public PlayerStatsWindowModel(IPlayerProvider playerProvider)
        {
            playerProvider.Player.View.Position
                .Subscribe(SetPosition)
                .AddTo(_disposables);
            
            playerProvider.Player.View.Rotation
                .Subscribe(SetRotation)
                .AddTo(_disposables);
            
            playerProvider.Player.View.Velocity
                .Subscribe(SetVelocity)
                .AddTo(_disposables);
            
            playerProvider.Player.View.Gun.LaserCharges
                .Subscribe(SetLaserCharges)
                .AddTo(_disposables);
            
            playerProvider.Player.View.Gun.CooldownLaser
                .Subscribe(SetLaserCooldown)
                .AddTo(_disposables);
        }
        
        private void SetPosition(Vector3 value) =>
            _position.Value = value;
        
        private void SetRotation(Quaternion value) =>
            _rotation.Value = value;
        
        private void SetVelocity(float value) =>
            _velocity.Value = value;
        
        private void SetLaserCharges(int value) =>
            _laserCharges.Value = value;
        
        private void SetLaserCooldown(float value) =>
            _laserCooldown.Value = value;
        
        public void Dispose() => 
            _disposables.Dispose();
    }
}