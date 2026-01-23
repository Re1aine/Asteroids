using _Project.Code.Logic.Gameplay.Audio;
using _Project.Code.Logic.Gameplay.Services.Configs.Configs.Assets;
using Cysharp.Threading.Tasks;

namespace _Project.Code.Logic.Gameplay.Services.Configs.AssetsConfigProvider
{
    public interface IAssetsConfigsProvider
    {
        UniTask Initialize();
        AudioConfig AudioConfig { get; }
        VFXConfig VFXConfig { get; }
    }
}