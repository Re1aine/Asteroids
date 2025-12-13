using System.Threading.Tasks;

public interface IConfigsProvider
{
    Task Initialize();
    AudioConfig GetAudioConfig();
}