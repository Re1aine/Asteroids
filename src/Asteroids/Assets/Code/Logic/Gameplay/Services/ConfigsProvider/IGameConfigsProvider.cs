using Code.Logic.Gameplay.Audio;
using Code.Logic.Gameplay.Services.ConfigsProvider;
using Cysharp.Threading.Tasks;

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