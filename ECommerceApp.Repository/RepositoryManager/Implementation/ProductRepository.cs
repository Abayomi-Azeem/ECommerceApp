using ECommerceApp.Domain.Dtos;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Domain.Enums;
using ECommerceApp.Repository.RepositoryManager.Interfaces;
using ECommerceApp.Repository.BaseRepository.Implementation;
namespace ECommerceApp.Repository.RepositoryManager.Implementation;

public class ProductRepository : Repository<Product>, IProductRepository
{
    private readonly ECommerceAppDbContext _dbContext;

    public ProductRepository(ECommerceAppDbContext dbContext): base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async  Task<bool>  AddProduct(NewProductDto productDetails)
    {
        Product? product = new Product().Create(productDetails);
        var result = await this.AddAsync(product);
        return result;
    }

    public List<Product> FilterByPrice(decimal upperbound, decimal lowerbound)
    {
        var products = this.GetByPredicate(x=> x.Price < upperbound && x.Price >= lowerbound).ToList();
        return products;
    }

    public List<Product> FilterByProductType(string productType)
    {
        var products = this.GetByPredicate(x=> x.Type.ToLower() == productType.ToLower()).ToList();
        return products;
    }

    public List<Product> FilterByRating(int rating)
    {
        var products = this.GetByPredicate(x=> x.Rating == rating).ToList();
        return products;    }

    public List<Product> GetAllProducts(int pageNumber, string orderTable = "Name")
    {        
        var products = this.GetAll().AsEnumerable().OrderBy(x=> GetTable(orderTable,x)).Skip((pageNumber-1)*50).Take(50).ToList();
        return products;
    }

    private object GetTable(string orderTable, Product product) 
    {
        switch(orderTable)
        {
            case "Name":
                return product.Name;
            case "Make":
                return product.Make;
            case "Model":
                return product.Model;
            case "Price":
                return product.Price;    
            default:
                return null;


        }
    }

    public  async Task<bool> RemoveProduct(Guid productId)
    {
        Product product = this.GetById(productId);
        if(product == null) return false;
        return await this.DeleteById(productId);
    }

    public Product ViewProduct(Guid productId)
    {
        return this.GetById(productId);
    }

    public async Task<bool?> RateProduct(Guid productId, int rating)
    {
        Product? product = this.GetById(productId);
        if (product==null) return null;
        var result = product.RateProduct(productId, rating);
        if(result) return await this.UpdateAsync(product);
        return false;
    }
}