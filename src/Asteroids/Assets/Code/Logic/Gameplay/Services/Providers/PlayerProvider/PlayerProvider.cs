using System.Threading.Tasks;
using Code.Logic.Gameplay.Entities.Player;
using Code.Logic.Gameplay.Services.Boundries;
using Code.Logic.Gameplay.Services.Factories.GameFactory;
using UnityEngine;

namespace Code.Logic.Gameplay.Services.Providers.PlayerProvider
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

        public async Task Initialize() => 
            Player = await _gameFactory.CreatePlayer(_boundaries.Center, Quaternion.identity);
    }
}