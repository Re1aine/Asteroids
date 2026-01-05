using Code.Logic.Menu.Services.Purchase.Produict;

namespace Code.Logic.Menu.Services.Purchase.Catalog
{
    public interface IPurchaseCatalog
    {
        void Initialize();
        PurchaseProduct GetProduct(ProductId id);
    }
}