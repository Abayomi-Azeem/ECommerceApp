using ECommerceApp.Contracts.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Api.Controllers;

[Route("user")]
[Authorize]
public class UserController : ApiController
{
    [Route("deleteuser")]
    [HttpDelete]
    public IActionResult DeleteUser()
    {
        return Ok(Array.Empty<string>());
    }

    // public IActionResult ForgotPassword(string email)
    // {
    //     return Ok(); 
    // }

    // public IActionResult ChangePassword(ChangePasswordRequest userdDetails)
    // {
    //     return Ok();
    // }

    // public IActionResult FundWallet()
    // {
    //     return Ok();
    // }
}