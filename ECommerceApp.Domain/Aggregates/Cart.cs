using System.ComponentModel.DataAnnotations;
using ECommerceApp.Domain.Entities;

namespace ECommerceApp.Domain.Aggregates;

public class Cart
{
    [Key]
    public Guid Id {get; set;}

    public decimal TotalPrice {get; set;}

    public Guid UserId {get; set;}

    public virtual User? User {get; set;}

    public virtual ICollection<ProductQuantity>? Products {get; set;}

    public ProductQuantity? AddToCart(Guid cartId ,Guid productId, int quantity, Product product)
    {
        if(cartId == Id && product!= null)
        {
            var productQty = new ProductQuantity().Create(productId, quantity, cartId);
            var productPrice = product.Price * quantity;
            TotalPrice += productPrice;
            return productQty;
        }
        return null;   
    }

    public Cart Create(Guid userId)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        return this;   
    }

    public bool? RemoveFromCart(Guid cartId ,Guid productId)
    {
        if(cartId == Id)
        {
            ProductQuantity? item = Products?.FirstOrDefault(x => x.ProductId == productId);
            return Products?.Remove(item);  
        }
        return false;  
    }

    public List<ProductQuantity>? ViewCart(Guid cartId)
    {
        if(Id == cartId)
        {
            return this.Products?.ToList();
        }
        return null;
    }

    public Cart? GetCart(Guid cartId)
    {
        if(cartId == Id)
        {
            return this;
        }
        return null;
    }
}