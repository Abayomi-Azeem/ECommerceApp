using ECommerceApp.Domain.Aggregates;
using ECommerceApp.Domain.Dtos;
using ECommerceApp.Domain.Entities;

namespace ECommerceApp.Repository.RepositoryManager.Interfaces;

public interface ICartRepository
{
    Task<bool> AddToCart(AddToCartDto request);

    Task<bool?> RemoveFromCart(Guid cartId, Guid productId);

    List<ProductQuantity>? ViewCart(Guid cartId);

    Guid? FindCart(Guid userId);
}