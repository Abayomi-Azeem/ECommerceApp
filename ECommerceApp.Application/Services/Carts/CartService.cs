using ECommerceApp.Domain.Aggregates;
using ECommerceApp.Contracts.Carts;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Repository.RepositoryManager.Interfaces;
using ErrorOr;
using MapsterMapper;
using ECommerceApp.Domain.Dtos;

namespace ECommerceApp.Application.Services.Carts;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IMapper _mapper;

    public CartService(ICartRepository cartRepository, IMapper mapper)
    {
        _cartRepository = cartRepository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<bool?>> AddToCart(AddToCartRequest productDetails)
    {
        var request = _mapper.Map<AddToCartDto>(productDetails);
        var response = await _cartRepository.AddToCart(request);
        if(response) return true;
        return Error.Failure("Failure", "Could not Add Product to Cart");

    }

    public async Task<ErrorOr<bool?>> RemoveFromCart(Guid cartId, Guid productId)
    {
        var response =  await _cartRepository.RemoveFromCart(cartId, productId);
        if(response == true) return true;
        return Error.Failure("Failure", "Could not Remove Product from Cart");
    }

    public ErrorOr<List<ProductQuantity>> ViewCart(Guid cartId)
    {
        var response  = _cartRepository.ViewCart(cartId);
        if(response != null) return response;
        return Error.Failure("Failure", "Error retrieving Cart");
    }
}