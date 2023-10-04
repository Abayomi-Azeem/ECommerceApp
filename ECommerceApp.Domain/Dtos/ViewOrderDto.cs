using ECommerceApp.Domain.Aggregates;
using ECommerceApp.Domain.Entities;

namespace ECommerceApp.Domain.Dtos;

public class ViewOrderDto
{
    public Order OrderDetails {get; set;}

    public List<ProductQuantity> Products {get; set;}
}