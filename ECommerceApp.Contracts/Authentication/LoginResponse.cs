namespace ECommerceApp.Contracts.Authentication;

public record LoginResponse(
    Guid Id,
    Guid? CartId, 
    Guid? WalletId,
    string FirstName,
    string LastName,
    string Email,
    string Token
);