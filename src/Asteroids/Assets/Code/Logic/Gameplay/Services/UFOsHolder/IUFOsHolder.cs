using System.Collections.Generic;
using Code.Logic.Gameplay.UFO;

namespace Code.Logic.Gameplay.Services.UFOsHolder
{
    public interface IUFOsHolder
    {
        IReadOnlyList<UFOPresenter> UFOs { get; }
        void Add(UFOPresenter ufo);
        void Remove(UFOPresenter ufo);
        void DestroyAll();
    }
}