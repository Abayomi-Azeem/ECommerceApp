using ECommerceApp.Domain.Aggregates;
using ECommerceApp.Domain.Dtos;
using ECommerceApp.Domain.Enums;

namespace ECommerceApp.Repository.RepositoryManager.Interfaces;

public interface IOrderRepository
{
    Task<Guid?> CreateOrder(CreateOrderDto orderDetails); 

    Task<bool?> MakePayment(Guid orderId, decimal Amount);  
    
    List<ViewOrderDto> MyOrders(Guid userId);

    OrderStatus OrderStatus(Guid orderId);

    //Admin
    List<ViewOrderDto> AllOrders(int pageNumber, string orderTable = "Name");

}