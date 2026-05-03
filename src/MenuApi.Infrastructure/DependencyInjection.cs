using MenuApi.Application.Abstractions;
using MenuApi.Infrastructure.Persistence;
using MenuApi.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MenuApi.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<IMenuRepository, MockMenuRepository>();
        return services;
    }
}
