using _Project.Code.Logic.Menu.Services.Purchase.Product;
using GamePush;

namespace _Project.Code.Logic.Menu.Services.Purchase.Catalog
{
    public interface IPurchaseCatalog
    {
        void Initialize();
        FetchProducts GetProduct(ProductId id);
    }
}