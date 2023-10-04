using ECommerceApp.Domain.Models;

namespace ECommerceApp.Domain.DomainObjects.User.ValueObjects;

public sealed class ProductId: ValueObject
{
    public Guid Value {get;}

    private ProductId(Guid value)
    {
        Value = value;
    }

    public static ProductId CreateProductId()
    {
        return new(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}