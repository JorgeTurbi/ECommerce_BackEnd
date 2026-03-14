

using Microsoft.EntityFrameworkCore;
using Store.Infrastructure.Persistence;
namespace Store.Api.Extensions;

public static class MigrationExtensions
{
    /// <summary>
    /// Aplica migraciones pendientes a la base de datos al iniciar la aplicación.
    /// </summary>
    public static void ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        dbContext.Database.Migrate();

    }
}