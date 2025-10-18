using UnityEngine;

namespace Code.UI.PlayerStatsWindow
{
    public class PlayerStatsWindowModel
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

        public void SetPosition(Vector3 value) => _position.Value = value;
        public void SetRotation(Quaternion value) => _rotation.Value = value;
        public void SetVelocity(float value) => _velocity.Value = value;
        public void SetLaserCharges(int value) => _laserCharges.Value = value;
        public void SetLaserCooldown(float value) => _laserCooldown.Value = value;
    }
}