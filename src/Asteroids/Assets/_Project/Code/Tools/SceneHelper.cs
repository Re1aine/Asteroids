using _Project.Code.Infrastructure.Common.SceneLoader;
using _Project.Code.Scopes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Code.Tools
{
    public sealed class SceneHelper : MonoBehaviour
    {
        private const GameScenes InitProjectScene = GameScenes.Project;
    
        private void Awake()
        {
            if (FindFirstObjectByType<ProjectScope>() != null)
                return;
        
            SceneManager.LoadScene(InitProjectScene.ToString());
        }
    }
}