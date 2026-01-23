using System;
using _Project.Code.Logic.Gameplay.Entities.Player;
using _Project.Code.Logic.Gameplay.Services.Boundries;
using _Project.Code.Logic.Gameplay.Services.Factories.GameFactory;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Code.Logic.Gameplay.Services.Providers.PlayerProvider
{
    public class PlayerProvider : IPlayerProvider
    {
        public event Action<PlayerPresenter> PlayerChanged;
        
        private readonly IGameFactory _gameFactory;
        private readonly IBoundaries _boundaries;
        public PlayerPresenter Player { get; set; }

        public PlayerProvider(IGameFactory gameFactory, IBoundaries boundaries)
        {
            _gameFactory = gameFactory;
            _boundaries = boundaries;
        }
        
        public async UniTask Initialize()
        {
            Player = await _gameFactory.CreatePlayer(_boundaries.Center, Quaternion.identity);
            PlayerChanged?.Invoke(Player);
        }
    }
}