using ECommerceApp.Application.Common.Interfaces.Authentication;
using ECommerceApp.Contracts.Authentication;
using ECommerceApp.Domain.Common.Errors;
using ECommerceApp.Domain.Dtos;
using ECommerceApp.Repository.RepositoryManager.Interfaces;
using ErrorOr;
using MapsterMapper;


namespace ECommerceApp.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userService;

    private readonly ICartRepository _cartRepsoitory;

    private readonly IWalletRepository _WalletRepository;
    private readonly IMapper _mapper;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userService, IMapper mapper, IWalletRepository walletRepository, ICartRepository cartRepsoitory)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userService = userService;
        _mapper = mapper;
        _WalletRepository = walletRepository;
        _cartRepsoitory = cartRepsoitory;
    }

    public ErrorOr<LoginResponse> Login(LoginRequest request)
    {
        var loginDto = _mapper.Map<LoginDto>(request);
        var user = _userService.AuthenticateUser(loginDto);
        if(user==null) return Errors.User.InvalidUsernamePassword;
        var token = _jwtTokenGenerator.GenerateToken(user.Id, user.FirstName, user.LastName);
        var cartId = _cartRepsoitory.FindCart(user.Id);
        var walletId = _WalletRepository.FindWallet(user.Id);
        LoginResponse response = _mapper.Map<LoginResponse>((user,token));
        return new LoginResponse(user.Id,cartId,walletId, user.FirstName, user.LastName, user.Email, token);
    }

    public async Task<ErrorOr<RegisterResponse>> Register(RegisterRequest request)
    {
        var createUserDto = _mapper.Map<CreateUserDto>(request);
        var newUser = await _userService.CreateUser(createUserDto);
        if(newUser==null) return Errors.User.DuplicateEmailError;
        return new RegisterResponse(newUser.FirstName, newUser.LastName, newUser.Email);
    }
}