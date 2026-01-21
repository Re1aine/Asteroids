using System;
using Code.Logic.Gameplay.Entities.Player;
using Cysharp.Threading.Tasks;

namespace Code.Logic.Gameplay.Services.Providers.PlayerProvider
{
    public interface IPlayerProvider
    {
        event Action<PlayerPresenter> PlayerChanged;
        PlayerPresenter Player { get; set; }
        UniTask Initialize();
    }
}