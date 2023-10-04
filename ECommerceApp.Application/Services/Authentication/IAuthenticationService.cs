using ECommerceApp.Contracts.Authentication;
using ErrorOr;

namespace ECommerceApp.Application.Services.Authentication;

public interface IAuthenticationService
{
    ErrorOr<LoginResponse> Login(LoginRequest request);
    Task<ErrorOr<RegisterResponse>> Register(RegisterRequest request);
}