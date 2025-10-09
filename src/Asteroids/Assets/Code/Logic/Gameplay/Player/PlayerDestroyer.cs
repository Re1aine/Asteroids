using Code.Logic.Gameplay.Services;
using UnityEngine;

namespace Code.Logic.Gameplay.Player
{
    public class PlayerDestroyer : IDestroyer
    {
        private readonly PlayerPresenter _playerPresenter;
    
        public PlayerDestroyer(PlayerPresenter presenter)
        {
            _playerPresenter = presenter;
        }

        public void Destroy(DamageType damageType) => 
            Object.Destroy(_playerPresenter.View.gameObject);
    }
}