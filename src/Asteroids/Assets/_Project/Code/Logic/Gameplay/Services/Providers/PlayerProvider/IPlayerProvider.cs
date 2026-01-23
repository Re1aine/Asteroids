using System;
using _Project.Code.Logic.Gameplay.Entities.Player;
using Cysharp.Threading.Tasks;

namespace _Project.Code.Logic.Gameplay.Services.Providers.PlayerProvider
{
    public interface IPlayerProvider
    {
        event Action<PlayerPresenter> PlayerChanged;
        PlayerPresenter Player { get; set; }
        UniTask Initialize();
    }
}