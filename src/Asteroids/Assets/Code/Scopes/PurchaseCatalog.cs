using System.Collections.Generic;

public class PurchaseCatalog : IPurchaseCatalog
{
    private readonly Dictionary<ProductId, PurchaseProduct> _products = new();

    public void Initialize()
    {
        InitializeCatalog();
    }

    public PurchaseProduct GetProduct(ProductId id) => 
        _products[id];

    private void InitializeCatalog()
    {
        _products[ProductId.AdsRemoval] = new PurchaseProduct()
        {
            Id = ProductId.AdsRemoval,
            Name = "AdsRemoval",
            Description = "Remove all ads permanently",
            Price = 5,
            IsSubscription = false,
            IsOneTime = false
        };
        
        _products[ProductId.Currency] = new PurchaseProduct()
        {
            Id = ProductId.Currency,
            Name = "100 Gold",
            Description = "Get 100 gold coins",
            Price = 5,
            IsOneTime = true,
            IsSubscription = false,
            RewardGold = 100
        };
    }
}