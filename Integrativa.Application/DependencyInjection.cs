using Integrativa.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Integrativa.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ProcessoService>();

        return services;
    }
}