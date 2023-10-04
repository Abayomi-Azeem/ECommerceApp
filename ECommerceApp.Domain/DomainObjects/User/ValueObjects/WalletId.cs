using ECommerceApp.Domain.Models;

namespace ECommerceApp.Domain.DomainObjects.User.ValueObjects;

public sealed class WalletId: ValueObject
{
    public Guid Value {get;}

    private WalletId(Guid value)
    {
        Value = value;
    }

    public static WalletId CreateWalletId()
    {
        return new(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}