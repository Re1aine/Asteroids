using System;
using System.Collections.Generic;
using Code.Logic.Menu.Services.Purchase.Product;
using GamePush;
using UnityEngine;

namespace Code.Logic.Menu.Services.Purchase.Catalog
{
    public class PurchaseCatalog : IPurchaseCatalog
    {
        private Dictionary<ProductId, FetchProducts> _products = new();

        public void Initialize() => 
            InitializeCatalog();

        public FetchProducts GetProduct(ProductId id) => 
            _products[id];

        private void InitializeCatalog()
        {
#if UNITY_EDITOR
            _products = new Dictionary<ProductId, FetchProducts>()
            {
                [ProductId.AdsRemoval] =  new FetchProducts()
                {
                    tag = "AdsRemoval",
                    name = "AdsRemoval",
                    description = "Remove all ads permanently",
                }
            };
#else
            GP_Payments.Fetch(OnFetchProductsSuccess, OnFetchProductsFailed);
#endif

        }

        private void OnFetchProductsSuccess(List<FetchProducts> products)
        {
            foreach (var product in products) 
                _products.Add(Enum.Parse<ProductId>(product.tag), product);
            
            Debug.Log("Fetch products success");
        }

        private void OnFetchProductsFailed()
        {
            Debug.Log("Failed to fetch products");
        }
    }
}