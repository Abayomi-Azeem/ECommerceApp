using ErrorOr;

namespace ECommerceApp.Domain.Common.Errors;

public static partial class Errors
{
    public static class User
    {
        public static Error DuplicateEmailError => Error.Conflict(code:"Duplicate Emails", description:"User with Email already exists");

        public static Error InvalidUsernamePassword => Error.NotFound("Invalid Email or Password", "Email or Password Incorrect");
    }
}