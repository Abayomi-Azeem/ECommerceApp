using ECommerceApp.Domain.Aggregates;
using ECommerceApp.Domain.Dtos;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Domain.Enums;

namespace ECommerceApp.Repository.RepositoryManager.Interfaces;

public interface IUserRepository
{
    Task<User?> CreateUser(CreateUserDto userDetails);

    Task<bool> DeleteUser(Guid userId);

    User? AuthenticateUser(LoginDto loginDetails);  

    Task<string?> ForgotPassword(string email);

    Task<bool?> ChangePassword(ChangePasswordDto passwordDetails);
 
}