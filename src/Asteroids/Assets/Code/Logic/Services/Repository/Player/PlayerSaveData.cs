using System;
using System.Collections.Generic;
using Code.Logic.Menu.Services.Purchase.Produict;

namespace Code.Logic.Services.Repository.Player
{
    [Serializable]
    public class PlayerSaveData
    {
        public int HighScore;
        public bool IsAdsRemoved;
        public List<PurchaseProduct> PurchasedProducts = new();
    }
}