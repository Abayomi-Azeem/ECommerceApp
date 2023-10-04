using ECommerceApp.Api.Errors;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace ECommerceApp.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddSingleton<ProblemDetailsFactory, ECommerceAppProblemDetailsFactory>();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        return services;
    }
}