using System;
using _Project.Code.Logic.Menu.Services.Purchase.Product;

namespace _Project.Code.Logic.Menu.Services.Purchase
{
    public interface IPurchaseService
    {
        event Action<string> OneTimePurchased;
        event Action<string> PermanentPurchased;
        void Initialize();
        void Purchase(ProductId id);
    }
}