namespace ECommerceApp.Domain.Entities;

public class Wallet
{
    public Guid Id {get; set;}
    
    public Guid UserId {get; set;}
       
    public virtual User? User {get; set;}

    public decimal Balance {get; set;}

    public Wallet Create(Guid userId)
    {   
        Id = Guid.NewGuid();
        UserId = userId;
        Balance = 0;
        return this;
    }

    public bool FundWallet(Guid walletId, decimal amount)
    {
        if(walletId == Id)
        {
            this.Balance += amount;
            return true;
        }
        return false;
    }

    public decimal? ViewWalletBalance(Guid walletId)
    {
        if(walletId == Id)
        {
            
            return this.Balance;
        }
        return null;
    }

    public bool PayWithWallet(Guid walletId, decimal amount)
    {
        if(walletId == Id)
        {
            this.Balance -= amount;
            return true;
        }
        return false;
    }
}