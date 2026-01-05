using System;
using System.Collections;
using Code.Infrastructure.Common.CoroutineService;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Code.Infrastructure.Common.SceneLoader
{
    public class SceneLoader : ISceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner) => 
            _coroutineRunner = coroutineRunner;

        public async UniTask LoadScene(GameScenes scene)
        {
            AsyncOperationHandle<SceneInstance> handle =
                Addressables.LoadSceneAsync(scene.ToString(), LoadSceneMode.Single, false);

            await handle.ToUniTask();
            Debug.Log($"Scene {scene} loaded.");
        
            await handle.Result.ActivateAsync().ToUniTask();
            Debug.Log($"Scene {scene} activated.");
        }

        public void LoadAnyScene(GameScenes scene, Action onLoaded) => 
            _coroutineRunner.StartCoroutine(LoadSAnySceneAsync(scene, onLoaded), CoroutineScopes.Global);

        private IEnumerator LoadSAnySceneAsync(GameScenes scene, Action onLoaded)
        {
            AsyncOperation asyncSceneLoading  = SceneManager.LoadSceneAsync(scene.ToString());
            asyncSceneLoading!.allowSceneActivation= false;
        
            yield return new WaitUntil(() => asyncSceneLoading!.isDone);
            Debug.Log($"Scene {scene} loaded.");
        
            asyncSceneLoading.allowSceneActivation = true;
            Debug.Log($"Scene {scene} activated.");
        
            yield return null;
            onLoaded?.Invoke();
        }
    }

    public enum GameScenes
    {
        Project = 0,
        Menu = 1,
        Gameplay = 2
    }
}