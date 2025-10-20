using System;
using System.Threading.Tasks;
using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;
using UnityEngine;

public class FireBaseAnalytics : IAnalytics
{
    public event Action GameStarted;
    public event Action LaserUsed;
    public event Action GameEnded;
    
    public FireBaseAnalytics()
    {
        Initialize();
        
    }
    private FirebaseApp _firebaseApp;
    
    public void Initialize()
    {
        FirebaseApp.CheckAndFixDependenciesAsync()
            .ContinueWithOnMainThread(OnDependencyStatusReceived);
    }
    
    private void OnDependencyStatusReceived(Task<DependencyStatus> task)
    {
        try
        {
            if (!task.IsCompletedSuccessfully)
                throw new Exception("Could not resolve all Firebase dependencies", task.Exception);
            
            if (task.Result != DependencyStatus.Available)
                throw new Exception($"Could not resolve all Firebase dependencies: {task.Result}");
            
            Debug.Log("Firebase initialized successfully");
            FirebaseAnalytics.LogEvent("Test");

        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }   
    }

    public void StartSession()
    {
        GameStarted?.Invoke();
    }

    public void SendLaserUsedEvent() => 
        LaserUsed?.Invoke();

    public void EndSession() => 
        GameEnded?.Invoke();
}