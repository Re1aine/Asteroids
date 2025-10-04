public class HUDProvider : IHUDProvider
{
    public HUD HUD { get; private set; }
    
    private readonly IGameFactory _gameFactory;

    public HUDProvider(IGameFactory gameFactory) => 
        _gameFactory = gameFactory;

    public void Initialize() => 
        HUD = _gameFactory.CreateHUD();
}