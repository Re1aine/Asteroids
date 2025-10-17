using UnityEngine;

namespace Code.UI.PlayerStatsWindow
{
    public class PlayerStatsWindowModel
    {
        public readonly R3.ReadOnlyReactiveProperty<Vector3> Position;
        public readonly R3.ReadOnlyReactiveProperty<Quaternion> Rotation;
        public readonly R3.ReadOnlyReactiveProperty<float> Velocity;
        public readonly R3.ReadOnlyReactiveProperty<int> LaserCharges;
        public readonly R3.ReadOnlyReactiveProperty<float> LaserCooldown;
        
        private readonly R3.ReactiveProperty<Vector3> _position;
        private readonly R3.ReactiveProperty<Quaternion> _rotation;
        private readonly R3.ReactiveProperty<float> _velocity;
        private readonly R3.ReactiveProperty<int> _laserCharges;
        private readonly R3.ReactiveProperty<float> _laserCooldown;
        
        public PlayerStatsWindowModel()
        {
            _position = new R3.ReactiveProperty<Vector3>();
            _rotation = new R3.ReactiveProperty<Quaternion>();
            _velocity = new R3.ReactiveProperty<float>();
            _laserCharges =  new R3.ReactiveProperty<int>();
            _laserCooldown = new R3.ReactiveProperty<float>();
    
            Position = _position.ToReadOnlyReactiveProperty();
            Rotation = _rotation.ToReadOnlyReactiveProperty();
            Velocity = _velocity.ToReadOnlyReactiveProperty();
            LaserCharges =  _laserCharges.ToReadOnlyReactiveProperty();
            LaserCooldown = _laserCooldown.ToReadOnlyReactiveProperty();
        }

        public void SetPosition(Vector3 value) => _position.Value = value;
        public void SetRotation(Quaternion value) => _rotation.Value = value;
        public void SetVelocity(float value) => _velocity.Value = value;
        public void SetLaserCharges(int value) => _laserCharges.Value = value;
        public void SetLaserCooldown(float value) => _laserCooldown.Value = value;
    }
}