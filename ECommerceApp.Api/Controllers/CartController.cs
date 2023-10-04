using ECommerceApp.Application.Services.Carts;
using ECommerceApp.Contracts.Carts;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Api.Controllers;

[Route("cart")]
public class CartController: ApiController
{
    private readonly ICartService _cartservice;

    public CartController(ICartService cartservice)
    {
        _cartservice = cartservice;
    }

    [Route("addtocart")]
    [HttpPost]
    public async Task<IActionResult> AddProductToCart(AddToCartRequest request)
    {
        var response = await _cartservice.AddToCart(request);
        return response.Match<IActionResult>(
            response=> Ok("Product Added to Cart"), 
            Error =>Problem(errors:Error)
            );
    }

    [Route("removefromcart")]
    [HttpPost]
    public async Task<IActionResult> RemoveFromCart(Guid cartId, Guid productId)
    {
        var response = await _cartservice.RemoveFromCart(cartId, productId);
        return response.Match<IActionResult>(
            response=> Ok("Product Removed from Cart"), 
            Error =>Problem(errors:Error)
            );
    }

    [Route("viewcart")]
    [HttpGet]
    public IActionResult ViewCart(Guid cartId)
    {
        var response =  _cartservice.ViewCart(cartId);
        return response.Match<IActionResult>(
            response=> Ok(response), 
            Error =>Problem(errors:Error)
            );
    }
}