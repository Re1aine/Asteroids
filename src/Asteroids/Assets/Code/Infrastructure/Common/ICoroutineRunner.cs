using System.Collections;
using UnityEngine;

public interface ICoroutineRunner
{
    Coroutine StartCoroutine(IEnumerator coroutine, CoroutineScopes scope);
    void StopCoroutine(Coroutine coroutine, CoroutineScopes scope);
    void StopCoroutines(CoroutineScopes scope);
}