namespace ECommerceApp.Contracts.Authentication;

public record RegisterRequest(
    string FirstName,
    string LastName,
    string Email,
    string Gender,
    string HomeAddress,
    string Password
);