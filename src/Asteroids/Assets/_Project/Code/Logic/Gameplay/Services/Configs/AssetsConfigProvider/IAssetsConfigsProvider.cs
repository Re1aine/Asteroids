using Code.Logic.Gameplay.Audio;
using Code.Logic.Gameplay.Services.Configs.Configs.Assets;
using Cysharp.Threading.Tasks;

namespace Code.Logic.Gameplay.Services.Configs.AssetsConfigProvider
{
    public interface IAssetsConfigsProvider
    {
        UniTask Initialize();
        AudioConfig AudioConfig { get; }
        VFXConfig VFXConfig { get; }
    }
}