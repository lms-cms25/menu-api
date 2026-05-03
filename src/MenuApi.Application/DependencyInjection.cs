using MenuApi.Application.Abstractions;
using MenuApi.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MenuApi.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IMenuService, MenuService>();
        return services;
    }
}
