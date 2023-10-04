using ErrorOr;

namespace ECommerceApp.Domain.Common.Errors;

public  static partial class Errors
{
    public static class Product
    {
        public static Error ProductNotFound => Error.NotFound(code:"Not Found", description:"Selected Product is Invalid");
    }
}