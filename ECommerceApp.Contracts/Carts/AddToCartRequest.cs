namespace ECommerceApp.Contracts.Carts;

public class AddToCartRequest
{
    public Guid CartId {get; set;}

    public Guid ProductId {get; set;}

    public int Quantity {get; set;}
}