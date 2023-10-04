using ECommerceApp.Contracts.Orders;
using ECommerceApp.Domain.Dtos;
using ECommerceApp.Domain.Enums;
using ECommerceApp.Repository.RepositoryManager.Interfaces;
using ErrorOr;
using MapsterMapper;

namespace ECommerceApp.Application.Services.Orders;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public OrderService(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<Guid?>> CreateOrder(CreateOrderRequest orderDetails)
    {
        var order = _mapper.Map<CreateOrderDto>(orderDetails);
        var response = await _orderRepository.CreateOrder(order);
        if(response ==null) return Error.Failure("Failure", "Error Creating Order");
        return  response;
    }

    public async Task<ErrorOr<bool>> MakePayment(Guid orderId, decimal amount)
    {
        var response = await _orderRepository.MakePayment(orderId, amount);
        if(response is true) return true;
        return Error.Failure("Failure", "Payment Unsuccessful");
    }

    public ErrorOr<List<ViewOrderDto>> ViewAllOrders(int pageNumber, string orderTable)
    {
        var result = _orderRepository.AllOrders(pageNumber, orderTable);
        if(result.Count>0) return result;
        return Error.NotFound(description:"Orders not found");
    }

    public ErrorOr<OrderStatus> ViewOrderStatus(Guid orderId)
    {
        var response = _orderRepository.OrderStatus(orderId);
        return response;
    }

    public ErrorOr<List<ViewOrderDto>> ViewUserOrders(Guid userId)
    {
        var result = _orderRepository.MyOrders(userId);
        if(result.Count>0) return result;
        return Error.NotFound(description:"Orders not found");
    }
}