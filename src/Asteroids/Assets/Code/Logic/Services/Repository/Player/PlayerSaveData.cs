using System;
using System.Collections.Generic;
using GamePush;

namespace Code.Logic.Services.Repository.Player
{
    [Serializable]
    public class PlayerSaveData
    {
        public int HighScore;
        public bool IsAdsRemoved;
        public List<FetchPlayerPurchases> PurchasedProducts = new();
    }
}