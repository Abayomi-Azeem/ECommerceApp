using ECommerceApp.Domain.Entities;

namespace ECommerceApp.Repository.RepositoryManager.Interfaces;

public interface IWalletRepository
{
    Task<bool>  FundWallet(Guid walletId, decimal amount);
    Wallet ViewWallet(Guid walletId);
    Guid? FindWallet(Guid userId);
}