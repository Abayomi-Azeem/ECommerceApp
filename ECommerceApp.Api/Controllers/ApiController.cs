using ECommerceApp.Api.Http;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Api.Controllers;


[ApiController]
public class ApiController : ControllerBase
{
    
    protected IActionResult Problem(List<Error> errors)
    {
        var firstError = errors[0];
        HttpContext.Items[HttpContextItemKey.Errors] = errors;
        var statusCode = firstError.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };

        return Problem(statusCode:statusCode, title:firstError.Description);
    }
}