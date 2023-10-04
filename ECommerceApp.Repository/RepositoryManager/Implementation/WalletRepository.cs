using ECommerceApp.Domain.Entities;
using ECommerceApp.Repository.BaseRepository.Implementation;
using ECommerceApp.Repository.RepositoryManager.Interfaces;

namespace ECommerceApp.Repository.RepositoryManager.Implementation;

public class WalletRepository : Repository<Wallet>, IWalletRepository

{
    private readonly ECommerceAppDbContext _dbContext;

    public WalletRepository(ECommerceAppDbContext dbContext): base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> FundWallet(Guid walletId, decimal amount)
    {
        Wallet? wallet = this.GetById(walletId);
        var result = wallet.FundWallet(walletId, amount);
        if(result) return await this.UpdateAsync(wallet);
        return false;
    }

    public Wallet ViewWallet(Guid walletId)
    {
        return this.GetById(walletId);
    }  

    public Guid? FindWallet(Guid userId)
    {
        var wallet = _dbContext.Wallets.FirstOrDefault(x => x.UserId == userId);
        if(wallet == null) return null;
        return wallet.Id;
    } 
}