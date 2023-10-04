using ECommerceApp.Application.Services.Products;
using ECommerceApp.Contracts.Products;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Api.Controllers;

[Route("product")]
public class ProductController : ApiController
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    //add product
    [HttpPost]
    [Route("addproduct")]
    public async Task<IActionResult> AddProduct([FromForm]NewProductRequest productDetails)
    {
        //validate request
        var result = await _productService.NewProduct(productDetails);
        return result.Match<IActionResult>(
            result=> Ok("Product Added Successfully"), 
            Error =>Problem(errors:Error)
            );
        
    }
    
    [HttpGet]
    [Route("listproducts")]
    public IActionResult ListProducts(int pageNumber, string? sortBy=null)
    {
        var response = _productService.GetProducts(pageNumber, sortBy);
        return response.Match<IActionResult>(
            response=> Ok(response), 
            Error =>Problem(errors:Error)
            );
    }
    
    //rate product
    [HttpPost]
    [Route("rateproduct")]
    public async Task<IActionResult> RateProduct(Guid productId, int rating)
    {
        var response = await _productService.RateProduct(rating,productId);
        return response.Match<IActionResult>(
            response=> Ok("Product Rating Successfully"), 
            Error =>Problem(errors:Error)
            );
    }
   
    //remove product
    [HttpDelete]
    [Route("removeproduct")]
    public async Task<IActionResult> RemoveProduct(Guid productId)
    {
        var response = await _productService.RemoveProduct(productId);
        return response.Match<IActionResult>(
            response => Ok("Product Removed Successfully"),
            Error => Problem(errors: Error)
        );
    }
}