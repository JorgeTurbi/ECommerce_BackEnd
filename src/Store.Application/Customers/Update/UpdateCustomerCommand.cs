namespace Store.Application.Customers.Update;

/// <summary>
/// Comando que representa la actualizacion de un cliente existente.
/// </summary>
public sealed record UpdateCustomerCommand(
    Guid CustomerId,
    string Name,
    string LastName,
    string PhoneNumber,
    string Identify,
    string Email,
    string Country,
    string Line1,
    string Line2,
    string City,
    string State,
    string ZipCode,
    bool Active);
