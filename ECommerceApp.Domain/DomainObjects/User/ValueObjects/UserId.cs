using ECommerceApp.Domain.Models;

namespace ECommerceApp.Domain.DomainObjects.User.ValueObjects;

public sealed class UserId: ValueObject
{
    public Guid Value {get;}

    private UserId(Guid value)
    {
        Value = value;
    }

    public static UserId CreateUserId()
    {
        return new(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}