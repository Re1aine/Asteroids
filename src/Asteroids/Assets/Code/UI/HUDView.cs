using System;
using Code.Logic.Gameplay.Services.GameFactory;
using Code.Logic.Gameplay.Services.PlayerProvider;
using Code.Logic.Gameplay.Services.ScoreCounter;
using Code.UI;
using UnityEngine;
using VContainer;

namespace Code.UI
{
    public class HUDView : MonoBehaviour
    {
        public event Action OnPlayerStatsWindowCreated;

        private IPlayerProvider _playerProvider;

        private PlayerStatsWindowPresenter _playerStatsWindow;

        [Inject]
        public void Construct(IPlayerProvider playerProvider)
        {
            _playerProvider = playerProvider;
        }
        
        public void Start()
        {
            OnPlayerStatsWindowCreated?.Invoke();
            
            Init();
        }

        private void Init()
        {
            _playerStatsWindow.Init(_playerProvider);
        }

        public void SetPlayerStatsWindow(PlayerStatsWindowPresenter playerStatsWindow) => 
            _playerStatsWindow = playerStatsWindow;
        

        public void Destroy() => Destroy(gameObject);
    }
}

public class HUDPresenter
{
    public HUDModel Model { get; private set; }
    public HUDView View { get; private set; }
    
    private IPlayerProvider _playerProvider;
    private IGameFactory _gameFactory;
    private IScoreCountService _scoreCountService;

    public HUDPresenter(HUDModel model, HUDView view)
    {
        Model = model;
        View = view;

        view.OnPlayerStatsWindowCreated += CreatePlayerStatsWindow;
        model.PlayerStatsWindow.OnValueChanged += view.SetPlayerStatsWindow;
    }

    public void Init(IGameFactory gameFactory, IScoreCountService scoreCountService)
    {
        _gameFactory = gameFactory;
        _scoreCountService = scoreCountService;
    }

    public void Destroy()
    {
        View.Destroy();
    }

    public void CreateLoseWindow()
    {
        LoseWindowPresenter loseWindow = _gameFactory.CreateLoseWindow(View.transform);
        Model.SetLoseWindow(loseWindow);
        loseWindow.Init(_scoreCountService);
    }

    public void DestroyLoseWindow()
    {
        Model.LoseWindow.Value.Destroy();
        
        Model.SetLoseWindow(null);
    }
    
    private void CreatePlayerStatsWindow() => 
        Model.SetPlayerStatsWindow(_gameFactory.CreatePlayerStatsWindow(View.transform));

    public void DestroyPlayerStatsWindow()
    {
        View.OnPlayerStatsWindowCreated -= CreatePlayerStatsWindow;
        
        Model.PlayerStatsWindow.OnValueChanged -= View.SetPlayerStatsWindow;

        Model.PlayerStatsWindow.Value.Destroy();
        
        Model.SetPlayerStatsWindow(null);
    }
}

public class HUDModel
{
    public readonly ReadOnlyReactiveProperty<PlayerStatsWindowPresenter> PlayerStatsWindow;
    public readonly ReadOnlyReactiveProperty<LoseWindowPresenter> LoseWindow;

    private readonly ReactiveProperty<PlayerStatsWindowPresenter> _playerStatsWindow;
    private readonly ReactiveProperty<LoseWindowPresenter> _loseWindow;
    
    public HUDModel()
    {
        _playerStatsWindow = new ReactiveProperty<PlayerStatsWindowPresenter>(null);
        _loseWindow = new ReactiveProperty<LoseWindowPresenter>(null);
        
        PlayerStatsWindow = new ReadOnlyReactiveProperty<PlayerStatsWindowPresenter>(_playerStatsWindow);
        LoseWindow = new ReadOnlyReactiveProperty<LoseWindowPresenter>(_loseWindow);
    }

    public void SetPlayerStatsWindow(PlayerStatsWindowPresenter window) => _playerStatsWindow.SetValue(window);
    public void SetLoseWindow(LoseWindowPresenter window) => _loseWindow.SetValue(window);
}
