using Code.GameFlow.States.Gameplay;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Code.UI.LoseWindow
{
    public class LoseWindowView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _currentScore;
        [SerializeField] private TextMeshProUGUI _highScore;
        [SerializeField] private Button _restart; 
        
        private GameplayStateMachine _gameplayStateMachine;

        [Inject]
        public void Construct(GameplayStateMachine gameplayStateMachine)
        {
            _gameplayStateMachine = gameplayStateMachine;
        }

        private void Start() => 
            _restart.onClick.AddListener(() => _gameplayStateMachine.Enter<GameplayStart>());

        public void SetScore(int value) =>
            _currentScore.text = value.ToString();

        public void SetHighScore(int value) =>
            _highScore.text = value.ToString(); 
        
        public void Destroy() =>
            Destroy(gameObject);
    }
}