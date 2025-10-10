using Code.GameFlow.States.Gameplay;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Code.UI.LoseWindow
{
    public class LoseWindowView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _score;
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
            _score.text = $"Score: {value.ToString()}";

        public void Destroy() => Destroy(gameObject);
    }
}