using ECommerceApp.Domain.DomainObjects.User.ValueObjects;
using ECommerceApp.Domain.Enums;
using ECommerceApp.Domain.Models;

namespace ECommerceApp.Domain.Common.Entities;

public sealed class Product: Entity<ProductId>
{
    private Product(ProductId id): base(id)
    {

    }
    public string Name {get; }

    public string Price {get; }

    public string Make {get; }

    public string Model {get; }

    public int Rating {get; }

    public ProductType Type {get; }

    public int QuantityAvailable {get; }

    public DateTime LastDateRestocked  {get; }

    public Product Create()
    {
        return new Product(ProductId.CreateProductId());
    }
}