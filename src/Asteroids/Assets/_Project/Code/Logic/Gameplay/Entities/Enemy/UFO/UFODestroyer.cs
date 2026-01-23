using _Project.Code.Logic.Gameplay.Services.ScoreCounter;
using UnityEngine;

namespace _Project.Code.Logic.Gameplay.Entities.Enemy.UFO
{
    public class UFODestroyer : IDestroyer
    {
        private readonly UFOPresenter _ufoPresenter;
        private readonly IScoreCountService _scoreCountService;

        public UFODestroyer(UFOPresenter ufoPresenter, IScoreCountService scoreCountService)
        {
            _ufoPresenter = ufoPresenter;
            _scoreCountService = scoreCountService;
        }

        public void Destroy(DamageType damageType)
        {
            _scoreCountService.Add(_ufoPresenter.Model.Config.CurrentValue.ScoreReward);
            Object.Destroy(_ufoPresenter.View.gameObject);
        }
    }
}