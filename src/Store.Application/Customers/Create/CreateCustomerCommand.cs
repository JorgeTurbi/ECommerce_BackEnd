namespace Store.Application.Customers.Create;

/// <summary>
/// Comando que representa la solicitud de creacion de un cliente.
/// </summary>
public sealed record CreateCustomerCommand(
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
