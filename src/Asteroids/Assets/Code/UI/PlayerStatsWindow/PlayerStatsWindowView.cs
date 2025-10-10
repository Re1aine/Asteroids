using System;
using TMPro;
using UnityEngine;

namespace Code.UI.PlayerStatsWindow
{
    public class PlayerStatsWindowView : MonoBehaviour
    {
        public event Action OnPlayerStatsChanged;
        
        [SerializeField] private TextMeshProUGUI _position;
        [SerializeField] private TextMeshProUGUI _rotation;
        [SerializeField] private TextMeshProUGUI _velocity;
        [SerializeField] private TextMeshProUGUI _laserCharges;
        [SerializeField] private TextMeshProUGUI _laserCooldown;
        
        private void Update()
        {
            OnPlayerStatsChanged?.Invoke();
        }

        public void SetPosition(Vector3 value) => 
            _position.text = $"Position - " + $"X: {value.x:0.00}" + $"Y: {value.y:0.00}";

        public void SetRotation(Quaternion value) => 
            _rotation.text = $"Rotation - {value.eulerAngles.z:0°}";

        public void SetVelocity(float value) =>
            _velocity.text = $"Velocity - {value:0.00}";

        public void SetLaserCharges(int value) =>
            _laserCharges.text = $"Laser Charges - {value}";

        public void SetLaserCooldown(float value) =>
            _laserCooldown.text = $"Laser Cooldown - {value:0}";

        public void Destroy() => Destroy(gameObject);
    }
}