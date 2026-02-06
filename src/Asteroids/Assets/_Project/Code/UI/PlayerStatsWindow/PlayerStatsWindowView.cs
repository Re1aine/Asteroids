using Cysharp.Threading.Tasks;
using LitMotion;
using TMPro;
using UnityEngine;

namespace _Project.Code.UI.PlayerStatsWindow
{
    public class PlayerStatsWindowView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _position;
        [SerializeField] private TextMeshProUGUI _rotation;
        [SerializeField] private TextMeshProUGUI _velocity;
        [SerializeField] private TextMeshProUGUI _laserCharges;
        [SerializeField] private TextMeshProUGUI _laserCooldown;

        [Header("AnimationSettings")]
        [SerializeField] private float _durationMoveAnim;

        private RectTransform _rect;

        private Vector2 _originPosition;
        private Vector2 _targetPosition;
        
        private void Awake()
        {
            _rect = GetComponent<RectTransform>();
            
            _originPosition = _rect.anchoredPosition; 
            _targetPosition = new Vector2(-_rect.anchoredPosition.x, _rect.anchoredPosition.y);
        }

        private void Start() => 
            PlayEntranceAnimation();

        public void SetPosition(Vector3 value) => 
            _position.text = $"Position - " + $"X: {value.x:0.00} " + $"Y: {value.y:0.00}";

        public void SetRotation(Quaternion value) => 
            _rotation.text = $"Rotation - {value.eulerAngles.z:0°}";

        public void SetVelocity(float value) =>
            _velocity.text = $"Velocity - {value:0.00}";

        public void SetLaserCharges(int value) =>
            _laserCharges.text = $"Laser Charges - {value}";

        public void SetLaserCooldown(float value) =>
            _laserCooldown.text = $"Laser Cooldown - {value:0}";

        public async UniTaskVoid Destroy()
        {
            await PlayHideAnimation();
            Destroy(gameObject);
        }

        private void PlayEntranceAnimation()
        {
            LMotion.Create(_originPosition, _targetPosition, _durationMoveAnim)
                .WithDelay(0.2f)
                .WithEase(Ease.OutBack)
                .Bind(pos => _rect.anchoredPosition = pos)
                .AddTo(this);
        }

        private UniTask PlayHideAnimation()
        {
            return LMotion.Create(_targetPosition, _originPosition, _durationMoveAnim)
                .WithDelay(0.2f)
                .WithEase(Ease.OutBack)
                .Bind(pos => _rect.anchoredPosition = pos)
                .AddTo(this)
                .ToUniTask();
        }
    }
}