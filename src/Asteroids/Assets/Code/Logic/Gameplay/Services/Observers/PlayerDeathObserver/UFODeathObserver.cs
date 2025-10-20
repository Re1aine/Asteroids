using Code.Logic.Gameplay.Entities.Enemy.UFO;

public class UFODeathObserver  : IUFODeathObserver
{
    private readonly UFOPresenter _ufoPresenter;
    private readonly IAnalyticsStore _analyticsStore;

    public UFODeathObserver(UFOPresenter ufoPresenter, IAnalyticsStore analyticsStore)
    {
        _ufoPresenter = ufoPresenter;
        _analyticsStore = analyticsStore;
    }

    public void Start() => 
        _ufoPresenter.Destroyed += OnDeath;

    public void Stop() => 
        _ufoPresenter.Destroyed -= OnDeath;

    private void OnDeath(UFOPresenter ufoPresenter) => 
        _analyticsStore.AddUfo();
}