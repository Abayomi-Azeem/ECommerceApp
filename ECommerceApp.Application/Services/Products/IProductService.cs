using ECommerceApp.Contracts.Products;
using ECommerceApp.Domain.Entities;
using ErrorOr;

namespace ECommerceApp.Application.Services.Products;

public interface IProductService
{
    Task<ErrorOr<bool>> NewProduct(NewProductRequest productDetails);

    Task<ErrorOr<bool>> RemoveProduct(Guid productId);

    ErrorOr<List<ListProduct>> GetProducts(int pageNumber, string sortBy);

    Task<ErrorOr<bool>> RateProduct(int rating, Guid productId);
 
}