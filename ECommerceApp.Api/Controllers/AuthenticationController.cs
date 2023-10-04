using Microsoft.AspNetCore.Mvc;
using ECommerceApp.Contracts.Authentication;
using ECommerceApp.Application.Services.Authentication;
using ECommerceApp.Application.Common.Interfaces.Authentication;
using ErrorOr;
using MapsterMapper;

namespace ECommerceApp.Api.Controllers;

[Route("auth")]
public class AuthenticationController : ApiController
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IMapper _mapper;

    public AuthenticationController(IAuthenticationService authenticationService, IMapper mapper)
    {
        _authenticationService = authenticationService;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request) 
    {
        
        ErrorOr<RegisterResponse> result = await _authenticationService.Register(request);
        return result.Match<IActionResult>(
            result=> Ok(result), 
            Error =>Problem(errors:Error)
            );
    }   

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        ErrorOr<LoginResponse> result = _authenticationService.Login(request);
        return result.Match<IActionResult>(
            result => Ok(result),
            Error =>Problem(errors:Error)
        );
    }
}