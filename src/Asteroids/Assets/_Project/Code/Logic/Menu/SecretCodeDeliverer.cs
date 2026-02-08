using LitMotion;
using TMPro;
using UnityEngine;

namespace _Project.Code.Logic.Menu
{
    public class SecretCodeDeliverer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _secretCodeText;
        [SerializeField] private TextMeshProUGUI _tipText;
        [SerializeField] private RectTransform _shipRect;
        [SerializeField] private RectTransform _delivererRoot;

        [SerializeField] private float baseSpeed;
        [SerializeField] private float speedVariation;
        [SerializeField] private float speedChangeFrequency;

        [SerializeField] private float _swayDuration;
        [SerializeField] private float _secretCodeSwayStrength;
        [SerializeField] private float _tipTextSwayStrength;
        
        private Vector2 _startAnchoredPosition;
        private Vector2 _moveDirection;
        
        private float _startTime;

        private void Awake() => 
            _delivererRoot = _delivererRoot.GetComponent<RectTransform>();

        private void Start()
        {
            _startTime = Time.time;
            _startAnchoredPosition = _delivererRoot.anchoredPosition;
            
            PlayTextAnimation();
        }

        private void Update()
        {
            HandleReplace();
            HandleMovement();
        }

        public void Destroy() => 
            Destroy(gameObject);

        public void SetSecretCode(string text) => 
            _secretCodeText.text = text;
        
        public void CopyToClipboard()
        {
            TextEditor editor =  new TextEditor();
            editor.text = _secretCodeText.text;
            editor.SelectAll();
            editor.Copy();
        }
        
        private void HandleReplace()
        {
            if (_delivererRoot.anchoredPosition.x <= -_startAnchoredPosition.x) 
                _delivererRoot.anchoredPosition = _startAnchoredPosition;
        }

        private void HandleMovement()
        {
            float timeSinceStart = Time.time - _startTime;
            float speedMultiplier = Mathf.Sin(timeSinceStart * speedChangeFrequency * Mathf.PI * 2);
            float currentSpeed = baseSpeed + (speedMultiplier * speedVariation);
            
            float angle = _shipRect.localEulerAngles.z * Mathf.Deg2Rad;
            _moveDirection = new Vector2(-Mathf.Sin(angle), Mathf.Cos(angle));
            
            _delivererRoot.anchoredPosition += _moveDirection * currentSpeed * Time.deltaTime;
        }

        private void PlayTextAnimation()
        {
            LMotion.Create(_secretCodeText.rectTransform.localEulerAngles.z, _secretCodeText.rectTransform.localEulerAngles.z + _secretCodeSwayStrength, _swayDuration)
                .WithEase(Ease.InOutSine)
                .WithLoops(-1, LoopType.Yoyo)
                .Bind(angle => 
                {
                    _secretCodeText.rectTransform.localEulerAngles = new Vector3(0, 0, angle);
                })
                .AddTo(this);

            LMotion.Create(_tipText.rectTransform.localEulerAngles.z, _tipText.rectTransform.localEulerAngles.z + _tipTextSwayStrength, _swayDuration)
                .WithEase(Ease.InOutSine)
                .WithDelay(0.2f)
                .WithLoops(-1, LoopType.Yoyo)
                .Bind(angle => 
                {
                    _tipText.rectTransform.localEulerAngles = new Vector3(0, 0, angle);
                })
                .AddTo(this);
        }
    }
}