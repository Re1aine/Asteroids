using _Project.Code.Logic.Gameplay.Services.Configs.Configs.Balance;
using Cysharp.Threading.Tasks;

namespace _Project.Code.Logic.Gameplay.Services.Configs.BalanceConfigsProvider
{
    public interface IBalanceConfigsProvider  
    {
        UniTask Initialize();
        PlayerConfig PlayerConfig { get; }
        AsteroidConfig AsteroidConfig { get; }
        GunConfig GunConfig { get; }
        UfoConfig UfoConfig { get; }
        UfoSpawnerConfig UfoSpawnerConfig { get; }
        AsteroidSpawnerConfig AsteroidSpawnerConfig { get; }
    }
}