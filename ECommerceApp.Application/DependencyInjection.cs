using ECommerceApp.Application.Mapping;
using ECommerceApp.Application.Services.Authentication;
using ECommerceApp.Application.Services.Carts;
using ECommerceApp.Application.Services.Orders;
using ECommerceApp.Application.Services.Products;
using ECommerceApp.Repository.RepositoryManager.Implementation;
using ECommerceApp.Repository.RepositoryManager.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceApp.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<ICartService, CartService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IWalletRepository, WalletRepository>();
        services.AddMappings();
        return services;
    }
}