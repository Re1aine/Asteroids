using Code.GameFlow.States.Gameplay;
using Code.Logic.Gameplay.Services.ScoreCounter;
using Code.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Code.UI
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

public class LoseWindowPresenter
{
    private readonly LoseWindowModel _loseWindowModel;
    private readonly LoseWindowView _loseWindowView;
    
    private IScoreCountService _scoreCountService;

    public LoseWindowPresenter(LoseWindowModel loseWindowModel, LoseWindowView loseWindowView)
    {
        _loseWindowModel = loseWindowModel;
        _loseWindowView = loseWindowView;
    }

    public void Init(IScoreCountService scoreCountService)
    {
        _scoreCountService = scoreCountService;
        
        _loseWindowModel.Score.OnValueChanged += _loseWindowView.SetScore;
        
        SetScore(_scoreCountService.Score);
    }

    private void SetScore(int value) => 
        _loseWindowModel.SetScore(value);

    public void Destroy()
    {
        _loseWindowModel.Score.OnValueChanged -= _loseWindowView.SetScore;
        _loseWindowView.Destroy();
    }
}

public class LoseWindowModel
{
    public readonly ReadOnlyReactiveProperty<int> Score;

    private readonly ReactiveProperty<int> _score;

    public LoseWindowModel()
    {
        _score = new ReactiveProperty<int>(0);
        
        Score = new ReadOnlyReactiveProperty<int>(_score);
    }

    public void SetScore(int value) => _score.SetValue(value);
}