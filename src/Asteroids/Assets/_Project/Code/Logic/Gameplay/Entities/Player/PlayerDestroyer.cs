using UnityEngine;

namespace _Project.Code.Logic.Gameplay.Entities.Player
{
    public class PlayerDestroyer : IDestroyer
    {
        private readonly PlayerPresenter _playerPresenter;
    
        public PlayerDestroyer(PlayerPresenter presenter) => 
            _playerPresenter = presenter;

        public void Destroy(DamageType damageType) => 
            Object.Destroy(_playerPresenter.View.gameObject);
    }
}