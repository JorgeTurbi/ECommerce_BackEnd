namespace Store.Domain.Primitives;

/// <summary>
/// Contrato base para un evento de dominio.
/// </summary>
public interface IDomainEvent
{
    /// <summary>
    /// Identificador unico del evento.
    /// </summary>
    Guid Id { get; }
}

/// <summary>
/// Implementacion base para eventos de dominio inmutables.
/// </summary>
public abstract record DomainEvent(Guid Id) : IDomainEvent;
