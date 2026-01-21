using TMPro;
using UnityEngine;

namespace Code.Logic.Menu
{
    public class SecretCodeDeliverer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _secretCodeText;
        [SerializeField] private GameObject _ship;
    
        [SerializeField] private float baseSpeed;
        [SerializeField] private float speedVariation;
        [SerializeField] private float speedChangeFrequency;
    
        private float _startTime;
        private Vector3 _startPosition;
    
        public void SetSecretCode(string text) => 
            _secretCodeText.text = text;

        private void Start()
        {
            _startTime = Time.time;
            _startPosition = transform.position;
        }

        private void Update()
        {
            HandleReplace();
            HandleMovement();
        }

        private void HandleReplace()
        {
            if (transform.position.x <= -_startPosition.x) 
                transform.position = _startPosition;
        }

        private void HandleMovement()
        {
            float timeSinceStart = Time.time - _startTime;
            float speedMultiplier = Mathf.Sin(timeSinceStart * speedChangeFrequency * Mathf.PI * 2);
            float currentSpeed = baseSpeed + (speedMultiplier * speedVariation);
        
            transform.position += _ship.transform.up * currentSpeed * Time.deltaTime;
        }
    }
}