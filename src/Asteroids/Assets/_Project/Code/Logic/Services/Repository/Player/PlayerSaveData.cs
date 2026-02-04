using System;
using System.Collections.Generic;
using GamePush;

namespace _Project.Code.Logic.Services.Repository.Player
{
    [Serializable]
    public class PlayerSaveData
    {
        public int HighScore;
        public bool IsAdsRemoved;
        public List<FetchPlayerPurchases> PurchasedProducts = new();
        public string LastSavedTime = "1970-01-01T00:00:00Z";
    }
}