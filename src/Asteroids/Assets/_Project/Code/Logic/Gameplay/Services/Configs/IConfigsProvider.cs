using _Project.Code.Logic.Gameplay.Audio;
using _Project.Code.Logic.Gameplay.Services.Configs.Configs.Assets;
using _Project.Code.Logic.Gameplay.Services.Configs.Configs.Balance;
using Cysharp.Threading.Tasks;

namespace _Project.Code.Logic.Gameplay.Services.Configs
{
    public interface IConfigsProvider
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