

using System.IO;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.Application.Data;
using Store.Domain.Customers;
using Store.Domain.Primitives;
using Store.Infrastructure.Persistence.Repositories;
using Store.Infrastructure.Persistence;
namespace Store.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);
        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        // Carga variables de entorno desde un .env cuando existe (útil en desarrollo local).
        // Busca el archivo en el directorio de trabajo actual y en todos los padres hasta la raíz.
        var envPath = FindDotEnvFile(Directory.GetCurrentDirectory());
        if (envPath != null)
        {
            Env.Load(envPath);
        }

        var connectionString = configuration.GetConnectionString("DatabaseConnection")
            ?? Environment.GetEnvironmentVariable("ConnectionStrings__DatabaseConnection");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException("No se encontró la cadena de conexión 'DatabaseConnection' en la configuración ni en las variables de entorno.");
        }

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<ICustomerRepository, CustomerRepository>();

        return services;
    }

    private static string? FindDotEnvFile(string startDirectory)
    {
        var dir = new DirectoryInfo(startDirectory);
        while (dir != null)
        {
            var candidate = Path.Combine(dir.FullName, ".env");
            if (File.Exists(candidate))
            {
                return candidate;
            }
            dir = dir.Parent;
        }

        return null;
    }
}