using System;
using System.Collections.Generic;

namespace Code.Logic.Gameplay.Services.Repository.Player
{
    [Serializable]
    public class PlayerSaveData
    {
        public int HighScore;
        public bool IsAdsRemoved;
        public List<PurchaseProduct> PurchasedProducts = new();
    }
}