using ECommerceApp.Domain.Aggregates;
using ECommerceApp.Domain.Dtos;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Domain.Enums;
using ECommerceApp.Repository.BaseRepository.Implementation;
using ECommerceApp.Repository.BaseRepository.Interfaces;
using ECommerceApp.Repository.RepositoryManager.Interfaces;

namespace ECommerceApp.Repository.RepositoryManager.Implementation;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    private readonly ECommerceAppDbContext _dbContext;

    public OrderRepository(ECommerceAppDbContext dbContext): base(dbContext)
    {
        _dbContext = dbContext;
    } 
    public List<ViewOrderDto> AllOrders(int pageNumber, string orderTable)
    {
        List<ViewOrderDto> ordersList = new();
        var orders = this.GetAll().AsEnumerable().OrderBy(x=> GetTable(orderTable,x)).Skip((pageNumber-1)*50).Take(50).ToList();
        foreach(Order order in orders)
        {
            var products = _dbContext.ProductQuantities.Where(x=> x.OrderId==order.Id).ToList();
            var viewOrder = new ViewOrderDto()
                                {
                                    OrderDetails = order,
                                    Products = products
                                };
            ordersList.Add(viewOrder);
        }
        return ordersList;
    }

     private object GetTable(string orderTable, Order order) 
    {
        switch(orderTable)
        {
            case "User":
                return order.UserId;
            case "Date":
                return order.DateCreated;
            case "PaymentStatus":
                return order.PaymentStatus;
            case "Price":
                return order.TotalPrice; 
            case "OrderStatus":
                return order.OrderStatus;
            default:
                return null;


        }
    }

    public async Task<Guid?> CreateOrder(CreateOrderDto orderDetails)
    {
        var cart = _dbContext.Carts.Find(orderDetails.CartId);
        if(cart==null) return null;
        //Cart? cart = new Cart().GetCart(orderDetails.CartId);
        var order = new Order().Create(cart, orderDetails.ShippingDetails);
        var productQuantites = _dbContext.ProductQuantities.Where(x=> x.CartId== orderDetails.CartId).ToList();
        foreach (ProductQuantity product in productQuantites)
        {
            product.OrderId = order.Id;
            _dbContext.Update(product);

        }
        await _dbContext.SaveChangesAsync();
        await this.AddAsync(order);
        return order.Id;
    }

    public async Task<bool?> MakePayment(Guid orderId, decimal amount)
    {
        Order order = this.GetById(orderId);
        Order? orderPayment = order.MakePayment(orderId, amount, 0, null);
        if(order != null) return await this.UpdateAsync(order);
        return null;        
    }

    public List<ViewOrderDto> MyOrders(Guid userId)
    {
        List<ViewOrderDto> ordersList = new();
        var userOrders = this.GetByPredicate(x=> x.UserId==userId).ToList();
        foreach(Order order in userOrders)
        {
            var products = _dbContext.ProductQuantities.Where(x=> x.OrderId==order.Id).ToList();
            var viewOrder = new ViewOrderDto()
                                {
                                    OrderDetails = order,
                                    Products = products
                                };
            ordersList.Add(viewOrder);
        }
        return ordersList;
    }

    public OrderStatus OrderStatus(Guid orderId)
    {
        var order = this.GetById(orderId);
        var orderStatus = order.ViewOrder(orderId).OrderStatus;
        return orderStatus;
    }
}