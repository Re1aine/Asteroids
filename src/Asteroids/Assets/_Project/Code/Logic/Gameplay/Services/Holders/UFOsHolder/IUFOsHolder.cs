using System.Collections.Generic;
using Code.Logic.Gameplay.Entities.Enemy.UFO;

namespace Code.Logic.Gameplay.Services.Holders.UFOsHolder
{
    public interface IUFOsHolder
    {
        IReadOnlyList<UFOPresenter> UFOs { get; }
        void Add(UFOPresenter ufo);
        void Remove(UFOPresenter ufo);
        void DestroyAll();
    }
}