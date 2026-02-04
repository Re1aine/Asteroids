using System.Collections.Generic;
using _Project.Code.Logic.Gameplay.Entities.Enemy.UFO;

namespace _Project.Code.Logic.Gameplay.Services.Holders.UFOsHolder
{
    public interface IUFOsHolder
    {
        IReadOnlyList<UFOPresenter> UFOs { get; }
        void Add(UFOPresenter ufo);
        void Remove(UFOPresenter ufo);
        void DestroyAll();
    }
}