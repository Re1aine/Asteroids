using Code.Logic.Gameplay.Audio;
using Code.Logic.Gameplay.Services.Configs.Configs.Assets;
using Code.Logic.Gameplay.Services.Configs.Configs.Balance;
using Cysharp.Threading.Tasks;

namespace Code.Logic.Gameplay.Services.Configs
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