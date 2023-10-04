using ECommerceApp.Contracts.Enums;
using Microsoft.AspNetCore.Http;

namespace ECommerceApp.Contracts.Products;

public class NewProductRequest
{
    public string Name {get; set;}

    public decimal Price {get; set;}

    public string Make {get; set;}

    public string Model {get; set;}

    public string Type {get; set;}

    public int QuantityAvailable {get; set;}

    public IFormFile Image {get; set;}

}