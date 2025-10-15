using System.Collections.Generic;
using UnityEngine;

namespace Code.Infrastructure.Common.AssetsManagement.AssetsLoader
{
    public class AssetsLoader : IAssetsLoader
    {
        private readonly Dictionary<string, MonoBehaviour> _cachedAssets = new();    
    
        public T Load<T>(string path) where T : MonoBehaviour
        {
            if (!_cachedAssets.ContainsKey(path)) 
                _cachedAssets[path] = Resources.Load<T>(path); 
        
            return (T)_cachedAssets[path];
        }
    }
}