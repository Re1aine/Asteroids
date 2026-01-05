public interface IPurchaseCatalog
{
    void Initialize();
    PurchaseProduct GetProduct(ProductId id);
}