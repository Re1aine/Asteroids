using System;
using System.Collections.Generic;

namespace Code.Logic.Services.SaveLoad.LocalStrategy.Storage
{
    [Serializable]
    public class PlayersSavesData
    {
        public List<PlayersSavesDataWrapper> Datas = new();
    }
}