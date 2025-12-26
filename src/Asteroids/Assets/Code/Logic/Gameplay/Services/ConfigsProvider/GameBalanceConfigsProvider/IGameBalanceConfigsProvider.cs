using Cysharp.Threading.Tasks;

public interface IGameBalanceConfigsProvider  
{
    UniTask Initialize();
    PlayerConfig PlayerConfig { get; }
    AsteroidConfig AsteroidConfig { get; }
    GunConfig GunConfig { get; }
    UfoConfig UfoConfig { get; }
    UfoSpawnerConfig UfoSpawnerConfig { get; }
    AsteroidSpawnerConfig AsteroidSpawnerConfig { get; }
}