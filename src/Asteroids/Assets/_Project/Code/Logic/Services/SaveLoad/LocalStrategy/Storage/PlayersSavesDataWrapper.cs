using System;
using _Project.Code.Logic.Services.Repository.Player;

namespace _Project.Code.Logic.Services.SaveLoad.LocalStrategy.Storage
{
    [Serializable]
    public class PlayersSavesDataWrapper
    {
        public string Key;
        public PlayerSaveData Data;
    }
}