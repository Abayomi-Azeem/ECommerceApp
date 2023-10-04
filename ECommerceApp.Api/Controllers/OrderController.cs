using ECommerceApp.Application.Services.Orders;
using ECommerceApp.Contracts.Orders;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Api.Controllers;

[Route("order")]
public class OrderController: ApiController
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    [Route("getallorders")]
    public IActionResult GetAllOrders(int pageNumber, string orderTable = "DateCreated")
    {
        var result =   _orderService.ViewAllOrders(pageNumber, orderTable);
        return result.Match<IActionResult>(
            result => Ok(result),
            Error => Problem(errors: Error)
        );
    }

    [HttpGet]
    [Route("viewuserorders")]
    public IActionResult ViewUserOrders(Guid userId)
    {
        var result = _orderService.ViewUserOrders(userId);
        return result.Match<IActionResult>(
            result => Ok(result),
            Error => Problem(errors: Error)
        );
    }

    [HttpPost]
    [Route("createorder")]
    public async Task<IActionResult> CreateOrder(CreateOrderRequest orderDetails)
    {
        var response = await _orderService.CreateOrder(orderDetails);
        return response.Match<IActionResult>(
            result => Ok(result),
            Error => Problem(errors: Error)
        );
    }

    [HttpPost]
    [Route("makepayment")]
    public async Task<IActionResult> MakePayment(Guid orderId, decimal amount)
    {
        var response = await _orderService.MakePayment(orderId, amount);
        return response.Match<IActionResult>(
            result => Ok(result),
            Error => Problem(errors: Error)
        );
    }

    [HttpGet]
    [Route("vieworderstatus")]
    public IActionResult ViewOrderStatus(Guid orderId)
    {
        var response =  _orderService.ViewOrderStatus(orderId);
        return response.Match<IActionResult>(
            result => Ok(result),
            Error => Problem(errors: Error)
        );
    }
}