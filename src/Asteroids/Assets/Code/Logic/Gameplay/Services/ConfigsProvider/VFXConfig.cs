using System;
using UnityEngine;

[CreateAssetMenu(fileName = "VFXConfig", menuName = "Configs/VFXConfig")]
public class VFXConfig : ScriptableObject
{
    public VFXSettings[] VFXS;

    public VFX GetVFXByType(VFXType type)
    {
        foreach (var vfx in VFXS)
        {
            if (vfx.Type == type)
                return vfx.Prefab;
        }
        
        throw new Exception($"Doesn't exist VFXSettings with VFXType : {type}");
    }
}