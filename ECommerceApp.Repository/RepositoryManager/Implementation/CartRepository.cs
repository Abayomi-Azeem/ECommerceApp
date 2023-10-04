using ECommerceApp.Domain.Aggregates;
using ECommerceApp.Domain.Dtos;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Repository.BaseRepository.Implementation;
using ECommerceApp.Repository.RepositoryManager.Interfaces;

namespace ECommerceApp.Repository.RepositoryManager.Implementation;

public class CartRepository : Repository<Cart>, ICartRepository
{
    private readonly ECommerceAppDbContext _dbcontext;

    public CartRepository(ECommerceAppDbContext dbcontext): base(dbcontext)
    {
        _dbcontext = dbcontext;
    }

    public async Task<bool> AddToCart(AddToCartDto request)
    {
        var cart = this.GetById(request.CartId);
        Product? product = _dbcontext.Products.Find(request.ProductId);
        var productQtyDetails = cart.AddToCart(request.CartId, request.ProductId, request.Quantity, product);
        if(productQtyDetails != null)
        {
            await _dbcontext.ProductQuantities.AddAsync(productQtyDetails);
            await _dbcontext.SaveChangesAsync();
            //return true;
            var res = await this.UpdateAsync(cart);
            return res;
        }
        return false;
    }

    public  async Task<bool?> RemoveFromCart(Guid cartId, Guid productId)
    {
        var cart = this.GetById(cartId);
        var product = _dbcontext.ProductQuantities.FirstOrDefault(x=> x.ProductId==productId);
        var isRemoved = _dbcontext.ProductQuantities.Remove(product);
        await _dbcontext.SaveChangesAsync();
        return true;       
    }

    
    public List<ProductQuantity>? ViewCart(Guid cartId)
    {
        var products = _dbcontext.ProductQuantities.Where(x=> x.CartId==cartId).ToList();
        return products;
    }

    public Guid? FindCart(Guid userId)
    {
        var cart = _dbcontext.Carts.FirstOrDefault(x => x.UserId == userId);
        if(cart == null) return null;
        return cart.Id;
    }

}