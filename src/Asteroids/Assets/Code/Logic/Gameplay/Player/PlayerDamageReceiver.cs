public class PlayerDamageReceiver : IDamageReceiver
{
    private readonly PlayerPresenter _playerPresenter;
    public PlayerDamageReceiver(PlayerPresenter presenter)
    {
        _playerPresenter = presenter;
    }

    public void ReceiverDamage(DamageType damageType)
    {
        if (damageType == DamageType.Asteroid || damageType == DamageType.AsteroidPart || damageType == DamageType.UFO)
        {
            _playerPresenter.DecrementHealth();
            
            if (_playerPresenter.Model.Health <= 0)
                _playerPresenter.Destroy(damageType);

        }
    }
}