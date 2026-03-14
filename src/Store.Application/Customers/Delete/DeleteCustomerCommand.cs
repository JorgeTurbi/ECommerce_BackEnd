namespace Store.Application.Customers.Delete;

/// <summary>
/// Comando que representa la eliminacion de un cliente existente.
/// </summary>
public sealed record DeleteCustomerCommand(Guid CustomerId);
