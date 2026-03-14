using Microsoft.EntityFrameworkCore;
using Store.Application.Data;
using Store.Domain.Customers;
using Store.Domain.Primitives;
using Wolverine;

namespace Store.Infrastructure.Persistence;



/// <summary>
/// Implementacion de <see cref="IApplicationDbContext"/> basada en Entity Framework Core.
/// Tambien actua como <see cref="IUnitOfWork"/> y publica eventos de dominio usando Wolverine.
/// </summary>
public sealed class ApplicationDbContext : DbContext, IApplicationDbContext, IUnitOfWork
{
    private readonly IMessageBus? _messageBus;

    /// <summary>
    /// Inicializa el contexto con sus opciones de EF Core y el bus de mensajes de Wolverine.
    /// </summary>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IMessageBus? messageBus = null) : base(options)
    {
        _messageBus = messageBus;
    }

    /// <summary>
    /// Conjunto de clientes persistidos en el sistema.
    /// </summary>
    public DbSet<Customer> Customers { get; set; }

    /// <summary>
    /// Aplica las configuraciones de entidades definidas en el ensamblado de Infrastructure.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Evitar que EF Core intente mapear los eventos de dominio como entidades.
        modelBuilder.Ignore<DomainEvent>();

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    /// <summary>
    /// Persiste cambios y publica los eventos de dominio generados por los agregados rastreados.
    /// </summary>
    public override async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        var domainEvents = ChangeTracker
            .Entries<AggregateRoot>()
            .Select(entry => entry.Entity)
            .SelectMany(aggregateRoot =>
            {
                var events = aggregateRoot.DomainEvents.ToList();
                aggregateRoot.ClearDomainEvents();
                return events;
            })
            .ToList();

        var result = await base.SaveChangesAsync(ct);

        foreach (var domainEvent in domainEvents)
        {
            if (_messageBus != null)
            {
                await _messageBus.PublishAsync(domainEvent);
            }
        }

        return result;
    }
}
