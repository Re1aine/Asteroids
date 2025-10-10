using UnityEngine;

namespace Code.UI.PlayerStatsWindow
{
    public class PlayerStatsWindowModel
    {
        public readonly ReadOnlyReactiveProperty<Vector3> Position;
        public readonly ReadOnlyReactiveProperty<Quaternion> Rotation;
        public readonly ReadOnlyReactiveProperty<float> Velocity;
        public readonly ReadOnlyReactiveProperty<int> LaserCharges;
        public readonly ReadOnlyReactiveProperty<float> LaserCooldown;
    
        private readonly ReactiveProperty<Vector3> _position;
        private readonly ReactiveProperty<Quaternion> _rotation;
        private readonly ReactiveProperty<float> _velocity;
        private readonly ReactiveProperty<int> _laserCharges;
        private readonly ReactiveProperty<float> _laserCooldown;
    
        public PlayerStatsWindowModel()
        {
            _position = new ReactiveProperty<Vector3>(new Vector3());
            _rotation = new ReactiveProperty<Quaternion>(Quaternion.identity);
            _velocity = new ReactiveProperty<float>(0);
            _laserCharges = new ReactiveProperty<int>(0);
            _laserCooldown = new ReactiveProperty<float>(0);
        
            Position = new ReadOnlyReactiveProperty<Vector3>(_position);
            Rotation = new ReadOnlyReactiveProperty<Quaternion>(_rotation);
            Velocity = new ReadOnlyReactiveProperty<float>(_velocity);
            LaserCharges = new ReadOnlyReactiveProperty<int>(_laserCharges);
            LaserCooldown = new ReadOnlyReactiveProperty<float>(_laserCooldown);
        }

        public void SetPosition(Vector3 value) => _position.SetValue(value);
        public void SetRotation(Quaternion value) => _rotation.SetValue(value);
        public void SetVelocity(float value) => _velocity.SetValue(value);
        public void SetLaserCharges(int value) => _laserCharges.SetValue(value);
        public void SetLaserCooldown(float value) => _laserCooldown.SetValue(value);
    
    }
}