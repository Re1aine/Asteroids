using UnityEngine;
using UnityEngine.SceneManagement;

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