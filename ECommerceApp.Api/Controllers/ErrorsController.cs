using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Api.Controllers;

public class ErrorsController : ControllerBase
{
    [Route("/error")]
    [HttpPost]
    public IActionResult Error()
    {
        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
        return Problem(title: exception?.InnerException.ToString(), statusCode:400);
    }
}