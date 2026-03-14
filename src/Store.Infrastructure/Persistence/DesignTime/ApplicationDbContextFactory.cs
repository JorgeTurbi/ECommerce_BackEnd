using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using DotNetEnv;

namespace Store.Infrastructure.Persistence.DesignTime;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        // Carga variables de entorno desde un .env (útil para dotnet ef en local).
        Env.Load();

        var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DatabaseConnection")
            ?? "Server=(localdb)\\mssqllocaldb;Database=StoreDb;Trusted_Connection=True;MultipleActiveResultSets=true";

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}
