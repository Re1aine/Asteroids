using Code.Logic.Gameplay.Audio;
using Code.Logic.Gameplay.Services.Configs.Configs.GameAssets;
using Cysharp.Threading.Tasks;

namespace Code.Logic.Gameplay.Services.Configs.GameAssetsConfigProvider
{
    public interface IGameAssetsConfigsProvider
    {
        UniTask Initialize();
        AudioConfig AudioConfig { get; }
        VFXConfig VFXConfig { get; }
    }
}