

using Microsoft.EntityFrameworkCore;
using Store.Infrastructure.Persistence;
namespace Store.Api.Extensions;

/// <summary>
/// Extensiones de conveniencia para operaciones relacionadas con la base de datos.
/// </summary>
public static class MigrationExtensions
{
    /// <summary>
    /// Aplica migraciones pendientes a la base de datos al iniciar la aplicación.
    /// </summary>
    /// <param name="app">La aplicación ASP.NET Core que ejecuta las migraciones.</param>
    public static void ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        dbContext.Database.Migrate();
    }
}