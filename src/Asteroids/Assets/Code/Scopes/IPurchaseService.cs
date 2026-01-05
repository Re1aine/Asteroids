using System;

public interface IPurchaseService
{
    event Action<ProductId> Purchased; 
    void Initialize();
    void Purchase(ProductId id);
}