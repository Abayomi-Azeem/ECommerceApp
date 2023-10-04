namespace ECommerceApp.Contracts.Authentication;

public record RegisterResponse(
    string FirstName,
    string LastName,
    string Email
);