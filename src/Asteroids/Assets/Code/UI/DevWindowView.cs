using System;
using Code.Logic.Gameplay.Services.Holders.RepositoriesHolder;
using Code.Logic.Services.Repository.Player;
using Code.Logic.Services.SaveLoad;
using Code.Logic.Services.SaveLoad.LocalStrategy;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Code.UI
{
    public class DevWindowView : MonoBehaviour
    {
        [SerializeField] private Button _manualSelectStrategy;
        [SerializeField] private Button _autoSelectStrategy;

        [SerializeField] private TextMeshProUGUI _playerHighScore;
        [SerializeField] private TextMeshProUGUI _playerHasPurchases;
        [SerializeField] private TextMeshProUGUI _playerLastSaveTime;

        private ISaveLoadService _saveLoadService;
        private PlayerRepository _playerRepository;

        private Image _autoImage;
        private Image _manualImage;
        private TextMeshProUGUI _manualSelectStrategyText;

        [Inject]
        public void Construct(ISaveLoadService saveLoadService, IRepositoriesHolder repositoriesHolder)
        {
            _saveLoadService = saveLoadService;
            _playerRepository = repositoriesHolder.GetRepository<PlayerRepository>();
        }

        private void Awake()
        {
            _autoImage = _autoSelectStrategy.GetComponent<Image>();
            _manualImage = _manualSelectStrategy.GetComponent<Image>();
            _manualSelectStrategyText = _manualSelectStrategy.GetComponentInChildren<TextMeshProUGUI>();
        }

        public void OnEnable()
        {
            _saveLoadService.IsAutoMode
                .DistinctUntilChanged()
                .Subscribe(x => UpdateModeVisual())
                .AddTo(this);

            _saveLoadService.StrategyChanged
                .Subscribe(x => ShowSaveStats())
                .AddTo(this);
        
            _autoSelectStrategy.onClick.AddListener(OnAutoSelectStrategy);
            _manualSelectStrategy.onClick.AddListener(OnManualSelectStrategy);
        
            ShowSaveStats();
        }

        private void OnAutoSelectStrategy() =>
            _saveLoadService.ResolveAutomatically();

        private void OnManualSelectStrategy()
        {
            _saveLoadService.SetAutoMode(false);

            if (_saveLoadService.Current is LocalSaveLoadStrategy)
            {
                _saveLoadService.UseCloud();
                _manualSelectStrategyText.text = "Cloud";
            }
            else
            {
                _saveLoadService.UseLocal();
                _manualSelectStrategyText.text = "Local";
            }
        }

        private void UpdateModeVisual()
        {
            _autoImage.color = _saveLoadService.IsAutoMode.CurrentValue ? Color.green : Color.white;
            _manualImage.color = _saveLoadService.IsAutoMode.CurrentValue ? Color.white : Color.green;
        }

        private void ShowSaveStats()
        {
            _playerHighScore.text = $"HighScore - {_playerRepository.HighScore}";
            _playerHasPurchases.text = $"Has Purchases - {(_playerRepository.PurchasedProducts.CurrentValue.Count > 0 ? "Yes" : "No")}";
        
            string rawTime = _playerRepository.LastSaveTime.CurrentValue;
            string displayTime = 
                string.IsNullOrWhiteSpace(rawTime) || rawTime == "1970-01-01T00:00:00Z"
                    ? "No Save Yet"
                    : DateTime.TryParse(rawTime, out var dt) 
                        ? $"{dt.ToLocalTime():dd MMM yyyy, HH:mm}"
                        : "Invalid date";

            _playerLastSaveTime.text = $"LastSaveTime - {displayTime}";
        }
    }
}