using ECommerceApp.Domain.Common.Entities;
using ECommerceApp.Domain.DomainObjects.User.ValueObjects;
using ECommerceApp.Domain.Models;

namespace ECommerceApp.Domain.DomainObjects.User.Entities;

public sealed class ProductItem: Entity<int>  
{
    
    private ProductItem(int id, ProductId product, int quantity): base(id)
    {
        Quantity = quantity;
        ProductId = product;
    }

    public int Quantity {get;}

    public ProductId ProductId;

    

    public static ProductItem Create( int id, ProductId productId, int quantity)
    {
        return new ( id, productId, quantity );
    }
}