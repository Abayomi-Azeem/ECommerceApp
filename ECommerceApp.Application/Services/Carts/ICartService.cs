using ECommerceApp.Domain.Aggregates;
using ECommerceApp.Contracts.Carts;
using ECommerceApp.Domain.Entities;
using ErrorOr;

namespace ECommerceApp.Application.Services.Carts;

public interface ICartService
{
   Task<ErrorOr<bool?>> AddToCart(AddToCartRequest request);

    Task<ErrorOr<bool?>> RemoveFromCart(Guid cartId, Guid productId);

    ErrorOr<List<ProductQuantity>> ViewCart(Guid cartId);
}