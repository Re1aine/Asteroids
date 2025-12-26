using Code.Logic.Gameplay.Audio;
using Cysharp.Threading.Tasks;

namespace Code.Logic.Gameplay.Services.ConfigsProvider
{
    public interface IGameAssetsConfigsProvider
    {
        UniTask Initialize();
        AudioConfig AudioConfig { get; }
        VFXConfig VFXConfig { get; }
    }
}