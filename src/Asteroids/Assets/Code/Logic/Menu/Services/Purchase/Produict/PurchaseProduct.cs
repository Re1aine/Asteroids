using System;

namespace Code.Logic.Menu.Services.Purchase.Produict
{
    [Serializable]
    public class PurchaseProduct
    {
        public ProductId Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsOneTime { get; set; }
        public bool IsSubscription { get; set; }
        public int RewardGold { get; set; }
    }
}