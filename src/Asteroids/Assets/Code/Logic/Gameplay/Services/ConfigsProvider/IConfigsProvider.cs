using Code.Logic.Gameplay.Audio;
using Cysharp.Threading.Tasks;

namespace Code.Logic.Gameplay.Services.ConfigsProvider
{
    public interface IConfigsProvider
    {
        UniTask Initialize();
        AudioConfig GetAudioConfig();
        VFXConfig GetVFXConfig();
    }
}