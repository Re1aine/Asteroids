using Code.Logic.Gameplay.Player;
using Code.Logic.Gameplay.Services.Boundries;
using Code.Logic.Gameplay.Services.GameFactory;
using UnityEngine;

namespace Code.Logic.Gameplay.Services.PlayerProvider
{
    public class PlayerProvider : IPlayerProvider
    {
        private readonly IGameFactory _gameFactory;
        private readonly IBoundaries _boundaries;
        public PlayerPresenter Player { get; set; }

        public PlayerProvider(IGameFactory gameFactory, IBoundaries boundaries)
        {
            _gameFactory = gameFactory;
            _boundaries = boundaries;
        }

        public void Initialize() => 
            Player = _gameFactory.CreatePlayer(_boundaries.Center, Quaternion.identity);
    }
}