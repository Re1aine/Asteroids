using Code.Logic.Menu.Services.Purchase.Product;
using GamePush;

namespace Code.Logic.Menu.Services.Purchase.Catalog
{
    public interface IPurchaseCatalog
    {
        void Initialize();
        FetchProducts GetProduct(ProductId id);
    }
}