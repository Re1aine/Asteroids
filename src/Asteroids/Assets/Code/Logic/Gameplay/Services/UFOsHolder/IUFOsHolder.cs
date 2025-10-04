using System.Collections.Generic;

public interface IUFOsHolder
{
    IReadOnlyList<UFOPresenter> UFOs { get; }
    void Add(UFOPresenter ufo);
    void Remove(UFOPresenter ufo);
    void DestroyAll();
}