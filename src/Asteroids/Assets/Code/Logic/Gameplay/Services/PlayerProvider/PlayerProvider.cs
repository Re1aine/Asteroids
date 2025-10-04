using UnityEngine;

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