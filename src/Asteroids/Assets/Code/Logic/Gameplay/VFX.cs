using System;
using UnityEngine;

public class VFX : MonoBehaviour
{
    public event Action<VFX> Destroyed;
    
    private ParticleSystem _particleSystem;

    private void Awake() =>
        _particleSystem = GetComponent<ParticleSystem>();

    public void Pause() =>
        _particleSystem.Pause();

    private void OnDestroy() => 
        Destroyed?.Invoke(this);

    public void Destroy() => 
        Destroy(gameObject);
}