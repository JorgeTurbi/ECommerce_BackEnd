using Microsoft.EntityFrameworkCore;
using Store.Domain.Customers;

namespace Store.Application.Data;

/// <summary>
/// Abstraccion del contexto de datos consumido por la capa Application para lecturas y persistencia simple.
/// </summary>
public interface IApplicationDbContext
{
    /// <summary>
    /// Conjunto de clientes persistidos en el sistema.
    /// </summary>
    DbSet<Customer> Customers { get; set; }

    /// <summary>
    /// Persiste los cambios pendientes en el almacenamiento.
    /// </summary>
    Task<int> SaveChangesAsync(CancellationToken ct);
}
