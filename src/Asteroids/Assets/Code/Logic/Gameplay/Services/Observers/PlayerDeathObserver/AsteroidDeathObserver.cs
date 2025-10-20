using Code.Logic.Gameplay.Entities.Enemy.Asteroid;

public class AsteroidDeathObserver : IAsteroidDeathObserver
{
    private readonly AsteroidPresenter _asteroidPresenter;
    private readonly IAnalyticsStore _analyticsStore;

    public AsteroidDeathObserver(AsteroidPresenter asteroidPresenter, IAnalyticsStore analyticsStore)
    {
        _asteroidPresenter = asteroidPresenter;
        _analyticsStore = analyticsStore;
    }

    public void Start()
    {
        _asteroidPresenter.Destroyed += OnDeath;
    }

    public void Stop()
    {
        _asteroidPresenter.Destroyed -= OnDeath;
    }

    private void OnDeath(AsteroidPresenter asteroidPresenter) => 
        _analyticsStore.AddAsteroid();
}