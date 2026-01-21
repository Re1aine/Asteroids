using Code.Logic.Gameplay.Analytics.AnalyticsStore;
using Code.Logic.Gameplay.Entities.Enemy.UFO;

namespace Code.Logic.Gameplay.Services.Observers.UFO
{
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
}