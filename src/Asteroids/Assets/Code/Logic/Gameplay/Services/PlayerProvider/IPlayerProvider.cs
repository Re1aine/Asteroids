public interface IPlayerProvider
{
    PlayerPresenter Player { get; set; }
    void Initialize();
}