using ECommerceApp.Domain.Common.Entities;
using ECommerceApp.Domain.DomainObjects.User.Entities;
using ECommerceApp.Domain.DomainObjects.User.ValueObjects;
using ECommerceApp.Domain.Models;

namespace ECommerceApp.Domain.DomainObjects.User;

public sealed class Cart: Entity<CartId>
{

    private readonly List<ProductItem> _productItems = new ();

    private Cart(CartId id, UserId userId) : base(id)
    {
        UserId = userId;
    }

    public decimal TotalPrice {get; }

    public int TotalProducts {get;}

    public UserId UserId {get;}

    public  IReadOnlyList<ProductItem> ProductItems => _productItems.AsReadOnly();

    public Cart Create(UserId id)
    {
        return new Cart(CartId.CreateCartId(), id);
    }

}