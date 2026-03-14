namespace Store.Domain.Primitives;

/// <summary>
/// Provee la base para los agregados raiz que publican eventos de dominio.
/// </summary>
public abstract class AggregateRoot
{
    private readonly List<DomainEvent> _domainEvents = new();

    /// <summary>
    /// Obtiene la coleccion de eventos de dominio pendientes.
    /// </summary>
    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    /// <summary>
    /// Registra un nuevo evento de dominio en el agregado.
    /// </summary>
    protected void AddDomainEvent(DomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    /// <summary>
    /// Limpia los eventos de dominio ya procesados.
    /// </summary>
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
