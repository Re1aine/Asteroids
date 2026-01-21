using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code.Logic.Gameplay.Services.Holders.VFXHolder
{
    public class VFXHolder : IVFXHolder 
    {
        public IReadOnlyList<VFX> VFXs => _vfxs;
    
        private readonly List<VFX> _vfxs = new();
    
        public void Add(VFX vfx)
        {
            _vfxs.Add(vfx);
            vfx.Destroyed += OnDestroyed;
        }

        public void Remove(VFX vfx)
        {
            _vfxs.Remove(vfx);
            vfx.Destroyed -= OnDestroyed;
        }

        public void DestroyAll()
        {
            _vfxs.ToList().ForEach(x => Object.Destroy(x.gameObject));
            _vfxs.Clear();
        
        }

        private void OnDestroyed(VFX vfx) =>
            Remove(vfx);
    }
}