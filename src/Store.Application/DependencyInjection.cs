using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Wolverine;

namespace Store.Application;

/// <summary>
/// Contiene la configuracion de dependencias de la capa Application.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registra Wolverine, handlers y validadores definidos en Application.
    /// </summary>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddWolverine(cfg =>
        {
            cfg.Discovery.IncludeAssembly(typeof(ApplicationAssemblyReference).Assembly);
        });

        services.AddValidatorsFromAssemblyContaining<ApplicationAssemblyReference>();

        return services;
    }
}
