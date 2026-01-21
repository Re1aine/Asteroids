using System;

namespace Code.Logic.Services.Authentification
{
    public interface IAuthentification
    {
        event Action AuthStarted;
        event Action AuthCompleted;
        void Initialize();
        void Login();
        void Logout();
    }
}