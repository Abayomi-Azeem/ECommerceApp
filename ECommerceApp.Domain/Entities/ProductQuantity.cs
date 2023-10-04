using System.ComponentModel.DataAnnotations;
using ECommerceApp.Domain.Aggregates;

namespace ECommerceApp.Domain.Entities;

public class ProductQuantity
{
    [Key]
    public int ProductQtyId {get; set;}
    public Guid ProductId {get; set;}
    public int Quantity {get; set;}
    public Guid CartId {get; set;}
    public Guid OrderId {get; set;}
    public virtual Cart? Cart {get; set;}

    public ProductQuantity Create(Guid productId, int quantity, Guid cartId)
    {
        ProductId = productId;
        Quantity = quantity;
        CartId = cartId;
        return this;
    }

    
}