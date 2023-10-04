using ECommerceApp.Application.Services.Products;
using ECommerceApp.Contracts.Products;
using ECommerceApp.Domain.Dtos;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Domain.Common.Errors;
using ECommerceApp.Repository.RepositoryManager.Interfaces;
using ErrorOr;
using MapsterMapper;

namespace ECommerceApp.Application.Services.Products;

public class ProductService : IProductService
{
    private readonly IProductRepository _productrepository;
    private readonly IMapper _mapper;


    public ProductService(IProductRepository productrepository, IMapper mapper)
    {
        _productrepository = productrepository;
        _mapper = mapper;
    }

    public ErrorOr<List<ListProduct>> GetProducts(int pageNumber, string sortBy)
    {
        var result = _productrepository.GetAllProducts(pageNumber, sortBy);
        if(result.Count>0) 
        {
            var response = result.Select(x => new ListProduct(){
                Id = x.Id,
                Name = x.Name,
                Make = x.Make,
                Model = x.Model,
                Price = x.Price,
                Rating = x.Rating,
                NoOfRatings = x.NoOfRatings,
                QuantityAvailable = x.QuantityAvailable,
                Image = Convert.ToBase64String(x.Picture),
                Type = x.Type
            }).ToList();
            return response;
        }
        return Error.NotFound(description:"Products not found");
    }

    public async Task<ErrorOr<bool>> NewProduct(NewProductRequest productDetails)
    {
        var productDto = new NewProductDto(){
            Name = productDetails.Name,
            Price = productDetails.Price,
            Make = productDetails.Make,
            Model = productDetails.Model,
            QuantityAvailable = productDetails.QuantityAvailable,
            Image = productDetails.Image,
            Type = productDetails.Type
        };
        var response = await _productrepository.AddProduct(productDto);
        if(response) return true;
        return Error.Failure(code:"Failure", description:"Error Adding Product");
    }

    public async Task<ErrorOr<bool>> RateProduct(int rating, Guid productId)
    {
        var response = await _productrepository.RateProduct(productId, rating);
        if(response == null) return Errors.Product.ProductNotFound;
        if(response== true) return true;
        return Error.Failure(code:"Failure", description: "Error Adding Rating");
    }

    public async Task<ErrorOr<bool>> RemoveProduct(Guid productId)
    {
        var response = await _productrepository.RemoveProduct(productId);
        if(response) return true;
        return Error.Failure(code: "Failure", description: "Error Removing Product");
    }
}