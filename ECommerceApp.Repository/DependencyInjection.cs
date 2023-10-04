using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;


namespace ECommerceApp.Repository;

public static class DependencyInjection
{
    public static IServiceCollection AddRepository(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<ECommerceAppDbContext>(options => 
            {
                options.UseSqlServer(configuration.GetConnectionString("ECommerceAppConn"),
                    sqlOptions => sqlOptions.EnableRetryOnFailure(50));
            });
        return services;
    }
}