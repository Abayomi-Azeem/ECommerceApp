using ECommerceApp.Domain.Models;

namespace ECommerceApp.Domain.DomainObjects.User.ValueObjects;

public sealed class CartId: ValueObject
{
    public Guid Value {get;}

    private CartId(Guid value)
    {
        Value = value;
    }

    public static CartId CreateCartId()
    {
        return new(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}