using System;
using Code.Logic.Menu.Services.Purchase.Produict;

namespace Code.Logic.Menu.Services.Purchase
{
    public interface IPurchaseService
    {
        event Action<ProductId> Purchased; 
        void Initialize();
        void Purchase(ProductId id);
    }
}