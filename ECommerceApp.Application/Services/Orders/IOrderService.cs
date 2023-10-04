using ECommerceApp.Contracts.Orders;
using ECommerceApp.Domain.Dtos;
using ECommerceApp.Domain.Enums;
using ErrorOr;

namespace ECommerceApp.Application.Services.Orders;

public interface IOrderService
{
    ErrorOr<List<ViewOrderDto>> ViewAllOrders(int pageNumber, string orderTable = "Name");

    Task<ErrorOr<Guid?>> CreateOrder(CreateOrderRequest orderDetails);

    Task<ErrorOr<bool>> MakePayment(Guid orderId, decimal amount);

    ErrorOr<List<ViewOrderDto>> ViewUserOrders(Guid userId);

    ErrorOr<OrderStatus> ViewOrderStatus(Guid orderId);

}