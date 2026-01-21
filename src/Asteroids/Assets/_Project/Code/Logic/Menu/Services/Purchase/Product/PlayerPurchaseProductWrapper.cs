using System;
using GamePush;

namespace Code.Logic.Menu.Services.Purchase.Product
{
    public class PlayerPurchaseProductWrapper
    {
        private readonly FetchProducts _product;
        private readonly FetchPlayerPurchases _playerPurchases = new();
    
        public PlayerPurchaseProductWrapper(FetchProducts product) => 
            _product = product;

        public FetchPlayerPurchases Wrap()
        {
            _playerPurchases.tag = _product.tag;
            _playerPurchases.productId =  _product.id;
            _playerPurchases.createdAt = DateTime.UtcNow.ToString("o");;
        
            return _playerPurchases;
        }
    }
}