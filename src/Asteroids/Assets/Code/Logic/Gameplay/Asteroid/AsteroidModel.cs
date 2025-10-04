using System;

public class AsteroidModel : ModelBase
{
    public event Action<AsteroidType> AsteroidTypeChanged;

    public int ScoreReward { get; }

    public AsteroidType AsteroidType
    {
        get => _asteroidType;
        private set
        {
            _asteroidType = value;
            AsteroidTypeChanged?.Invoke(value);
        }
    }

    private AsteroidType _asteroidType;

    public AsteroidModel(AsteroidType asteroidType, int scoreReward)
    {
        _asteroidType = asteroidType;
        ScoreReward = scoreReward;
    }
}