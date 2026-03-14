namespace Store.Domain.Primitives;

/// <summary>
/// Define la unidad de trabajo encargada de confirmar cambios del dominio.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Persiste los cambios pendientes.
    /// </summary>
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}
