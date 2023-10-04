
using ECommerceApp.Application.Common.Interfaces.Services;

namespace ECommerceApp.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}