using System;
using Code.Logic.Gameplay.Services.PlayerProvider;
using Code.UI.PlayerStatsWindow;
using UnityEngine;
using VContainer;

namespace Code.UI.HUD
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