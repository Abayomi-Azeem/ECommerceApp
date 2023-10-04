using ECommerceApp.Domain.DomainObjects.User.ValueObjects;
using ECommerceApp.Domain.Models;

namespace ECommerceApp.Domain.DomainObjects.User.Entities;

public sealed class Wallet: Entity<WalletId>
{
    
    private Wallet(UserId userId): base(WalletId.CreateWalletId())
    {
        UserId = userId;
    }
    public decimal Balance {get; }
    public UserId UserId {get;}

    public Wallet Create(UserId userId)
    {
        return new(userId);
    }
    // public Wallet Create(Guid userId)
    // {   
    //     Id = Guid.NewGuid();
    //     UserId = userId;
    //     Balance = 0;
    //     return this;
    // }
}