public class UfoDamageReceiver : IDamageReceiver
{
    private readonly UFOPresenter _ufoPresenter;
    public UfoDamageReceiver(UFOPresenter presenter)
    {
        _ufoPresenter = presenter;
    }
    
    public void ReceiverDamage(DamageType damageType)
    {
        _ufoPresenter.Destroy(damageType);
    }
}