using System;

public interface IAuthentification
{
    event Action AuthStarted;
    event Action AuthCompleted;
    void Initialize();
    void Login();
    void Logout();
}