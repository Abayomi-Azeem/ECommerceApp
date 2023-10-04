using ECommerceApp.Contracts.Authentication;
using ECommerceApp.Domain.Entities;
using Mapster;

namespace ECommerceApp.Application.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(User user, string token), LoginResponse>()
        .Map(dest=> dest.Token, src=> src.token)
        .Map(dest=> dest, src=> src.user);
    }
}