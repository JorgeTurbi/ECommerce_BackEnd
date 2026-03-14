namespace Store.Application.Customers.GetById;

/// <summary>
/// Query que solicita el detalle de un cliente por identificador.
/// </summary>
public sealed record GetCustomerByIdQuery(Guid CustomerId);
