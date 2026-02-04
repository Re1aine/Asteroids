using _Project.Code.GameFlow.States.Core;
using _Project.Code.GameFlow.States.Gameplay;
using _Project.Code.Infrastructure.Common.SceneLoader;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Project.Code.UI.LoseWindow
{
    public class LoseWindowView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _currentScore;
        [SerializeField] private TextMeshProUGUI _highScore;
        [SerializeField] private Button _restart; 
        [SerializeField] private Button _menu;
        
        private GameplayStateMachine _gameplayStateMachine;

        [Inject]
        public void Construct(GameplayStateMachine gameplayStateMachine)
        {
            _gameplayStateMachine = gameplayStateMachine;
        }

        private void Start()
        {
            _restart.onClick.AddListener(() => _gameplayStateMachine.Enter<GameplayInitState>().Forget());
            _menu.onClick.AddListener(() => _gameplayStateMachine.Enter<LoadSceneState, GameScenes>(GameScenes.Menu).Forget());
        }

        public void SetScore(int value) =>
            _currentScore.text = value.ToString();

        public void SetHighScore(int value) =>
            _highScore.text = value.ToString(); 
        
        public void Destroy() =>
            Destroy(gameObject);
    }
}