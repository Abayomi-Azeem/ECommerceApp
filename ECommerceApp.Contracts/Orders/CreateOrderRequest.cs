namespace ECommerceApp.Contracts.Orders;

public class CreateOrderRequest
{
    public Guid CartId {get; set;}

    public string ShippingDetails {get; set;}
}