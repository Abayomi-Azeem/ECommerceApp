namespace ECommerceApp.Domain.Dtos;

public class AddToCartDto
{
    public Guid CartId {get; set;}

    public Guid ProductId {get; set;}

    public int Quantity {get; set;}
}