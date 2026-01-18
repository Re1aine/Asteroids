using System;
using Code.Logic.Services.Repository.Player;

namespace Code.Logic.Services.SaveLoad.LocalStrategy.Storage
{
    [Serializable]
    public class PlayersSavesDataWrapper
    {
        public string Key;
        public PlayerSaveData Data;
    }
}