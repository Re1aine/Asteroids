using System;
using _Project.Code.Logic.Gameplay.Services.Providers.PlayerProvider;
using R3;
using UnityEngine;

namespace _Project.Code.UI.PlayerStatsWindow
{
    public class PlayerStatsWindowModel : IDisposable
    {
        public ReadOnlyReactiveProperty<Vector3> Position => _position;
        public ReadOnlyReactiveProperty<Quaternion> Rotation => _rotation;
        public ReadOnlyReactiveProperty<float> Velocity => _velocity;
        public ReadOnlyReactiveProperty<int> LaserCharges => _laserCharges;
        public ReadOnlyReactiveProperty<float> LaserCooldown => _laserCooldown;
        
        private readonly ReactiveProperty<Vector3> _position = new();
        private readonly ReactiveProperty<Quaternion> _rotation = new();
        private readonly ReactiveProperty<float> _velocity = new();
        private readonly ReactiveProperty<int> _laserCharges = new();
        private readonly ReactiveProperty<float> _laserCooldown = new();
        
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