using Cysharp.Threading.Tasks;

public interface IConfigsProvider
{
    UniTask Initialize();
    AudioConfig GetAudioConfig();
    VFXConfig GetVFXConfig();
}