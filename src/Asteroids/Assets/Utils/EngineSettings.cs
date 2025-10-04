using UnityEngine;

public class EngineSettings : MonoBehaviour
{
    public int TargetFPS = 100;

    void Awake()
    {
        Application.targetFrameRate = TargetFPS;
        Time.fixedDeltaTime = 1f / TargetFPS;
    }
}