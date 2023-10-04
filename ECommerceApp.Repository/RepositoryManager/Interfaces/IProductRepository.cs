using ECommerceApp.Domain.Dtos;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Domain.Enums;

namespace ECommerceApp.Repository.RepositoryManager.Interfaces;

public interface IProductRepository
{
    List<Product> GetAllProducts(int pageNumber, string orderTable = "Name");

    List<Product> FilterByPrice(decimal upperbound, decimal lowerbound);

    List<Product> FilterByProductType(string productType);
    
    List<Product> FilterByRating(int rating);

    Product ViewProduct(Guid productId);

    Task<bool?> RateProduct(Guid productId, int rating); 

    //Admin
    Task<bool>  AddProduct(NewProductDto product);

    Task<bool>  RemoveProduct(Guid productId);
}