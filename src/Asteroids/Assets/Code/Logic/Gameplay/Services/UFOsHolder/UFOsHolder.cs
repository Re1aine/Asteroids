using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UFOsHolder : IUFOsHolder
{
    public IReadOnlyList<UFOPresenter> UFOs => _ufos;
    
    private readonly List<UFOPresenter> _ufos = new();

    public void Add(UFOPresenter ufo) => _ufos.Add(ufo);
    public void Remove(UFOPresenter ufo) => _ufos.Remove(ufo);
    public void DestroyAll()
    {
        _ufos.ToList().ForEach(x => Object.Destroy(x.View.gameObject));
        _ufos.Clear();
    }
}