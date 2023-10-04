using ECommerceApp.Domain.Aggregates;

namespace ECommerceApp.Domain.Dtos;

public class CreateOrderDto
{
    public Guid CartId {get; set;}

    public string ShippingDetails {get; set;}
}