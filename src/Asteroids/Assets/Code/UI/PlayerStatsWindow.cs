using TMPro;
using UnityEngine;
using VContainer;

public class PlayerStatsWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _position;
    [SerializeField] private TextMeshProUGUI _rotation;
    [SerializeField] private TextMeshProUGUI _velocity;
    [SerializeField] private TextMeshProUGUI _laserCharges;
    [SerializeField] private TextMeshProUGUI _laserCooldown;

    private IPlayerProvider _playerProvider;
    
    [Inject]
    public void Construct(IPlayerProvider playerProvider)
    {
        _playerProvider = playerProvider;
    }

    private void Update()
    {
        SetPosition(_playerProvider.Player.View.transform.position);
        SetRotation(_playerProvider.Player.View.transform.rotation);
        SetVelocity(_playerProvider.Player.View.GetVelocity());
        SetLaserCharges(_playerProvider.Player.View.GetLaserCharges());
        SetLaserCooldown(_playerProvider.Player.View.GetLaserCooldown());
    }

    private void SetPosition(Vector3 value) => 
        _position.text = $"Position - " + $"X: {value.x:0.00}" + $"Y: {value.y:0.00}";

    private void SetRotation(Quaternion value) => 
        _rotation.text = $"Rotation - {value.eulerAngles.z:0°}";

    private void SetVelocity(float value) =>
        _velocity.text = $"Velocity - {value:0.00}";

    private void SetLaserCharges(int value) =>
        _laserCharges.text = $"Laser Charges - {value}";

    private void SetLaserCooldown(float value) =>
        _laserCooldown.text = $"Laser Cooldown - {value:0}";

    public void Destroy() => Destroy(gameObject);
}