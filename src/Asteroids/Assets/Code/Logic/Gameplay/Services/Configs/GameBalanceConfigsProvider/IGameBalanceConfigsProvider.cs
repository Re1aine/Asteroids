using Code.Logic.Gameplay.Services.Configs.Configs.GameBalance;
using Cysharp.Threading.Tasks;

namespace Code.Logic.Gameplay.Services.Configs.GameBalanceConfigsProvider
{
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
}