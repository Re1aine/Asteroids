using System.Collections;
using UnityEngine;

namespace Code.Infrastructure.Common.CoroutineService
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine, CoroutineScopes scope);
        void StopCoroutine(Coroutine coroutine, CoroutineScopes scope);
        void StopCoroutines(CoroutineScopes scope);
    }
}