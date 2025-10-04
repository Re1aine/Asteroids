using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class LoseWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private Button _restart; 
    
    private IScoreCountService _scoreCountService;
    private GameplayStateMachine _gameplayStateMachine;

    [Inject]
    public void Construct(IScoreCountService scoreCountService, GameplayStateMachine gameplayStateMachine)
    {
        _scoreCountService = scoreCountService;
        _gameplayStateMachine = gameplayStateMachine;
    }

    private void Start()
    {
        SetScore(_scoreCountService.Score);
        _restart.onClick.AddListener(() => _gameplayStateMachine.Enter<GameplayStart>());
    }

    private void SetScore(int value) => 
        _score.text = $"Score: {value.ToString()}";

    public void Destroy() => Destroy(gameObject);
}