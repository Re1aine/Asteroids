using System;

namespace Code.Logic.Gameplay.Services.ConfigsProvider
{
    [Serializable]
    public struct VFXSettings
    {
        public VFXType Type;
        public VFX Prefab;
    }
}