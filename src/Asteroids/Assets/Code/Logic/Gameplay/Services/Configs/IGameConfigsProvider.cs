using Code.Logic.Gameplay.Audio;
using Code.Logic.Gameplay.Services.Configs.Configs.GameAssets;
using Code.Logic.Gameplay.Services.Configs.Configs.GameBalance;
using Cysharp.Threading.Tasks;

namespace Code.Logic.Gameplay.Services.Configs
{
    public interface IGameConfigsProvider
    {
        UniTask Initialize();
        AudioConfig AudioConfig { get; }
        VFXConfig VFXConfig { get; } 
        PlayerConfig PlayerConfig { get; }
        AsteroidConfig AsteroidConfig { get; }
        GunConfig GunConfig { get; }
        UfoConfig UfoConfig { get; }
        UfoSpawnerConfig UfoSpawnerConfig { get; }
        AsteroidSpawnerConfig AsteroidSpawnerConfig { get; }
    }
}