using ECommerceApp.Domain.Dtos;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Domain.Enums;

namespace ECommerceApp.Domain.Aggregates;

public class Order
{
    public Guid Id {get; set;}

    public DateTime DateCreated {get; set;}

    public PaymentStatus PaymentStatus {get; set;}

    public string ShippingAddress {get; set;}

    public OrderStatus OrderStatus {get; set;}
    public decimal TotalPrice {get; set;}

    public decimal AmountPaid {get; set;}

    public decimal Discount {get; set;}

    public string? DiscountCode {get; set;}

    public Guid UserId {get; set;}

    public virtual User? User {get; set;}

   // public virtual  ICollection<ProductQuantity> Products {get; set;}

    public Order Create(Cart cart, string shippingDetails)
    {
       // var cart = new Cart().GetCart(orderDetails.CartId);
        Id = Guid.NewGuid();
        DateCreated = DateTime.Now;
        PaymentStatus = PaymentStatus.Pending;
        TotalPrice = cart.TotalPrice;
        UserId = cart.UserId;
        ShippingAddress = shippingDetails;
        return this;
    }

    public bool AddShippingAddress(Guid orderId, string shippingAddress)
    {
        if(orderId == Id)
        {
            ShippingAddress = shippingAddress;
            return true;
        }
        return false;
    }

    public Order? MakePayment(Guid orderId, decimal amountPaid, decimal discount, string discountCode)
    {
        if(orderId == Id)
        {
            PaymentStatus = PaymentStatus.Success;
            AmountPaid = amountPaid;
            Discount =discount;
            DiscountCode = discountCode;
            OrderStatus = OrderStatus.Ordered;
            return this;
        }
        return null;
    }

    public Order? ViewOrder(Guid orderId)
    {
        if(Id== orderId) return this;
        return null;
    }
}