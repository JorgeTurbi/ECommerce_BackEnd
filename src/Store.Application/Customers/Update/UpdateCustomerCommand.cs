using Store.Domain.Enums;

namespace Store.Application.Customers.Update;

/// <summary>
/// Comando que representa la actualizacion de un cliente existente.
/// </summary>
public sealed record UpdateCustomerCommand(
    Guid CustomerId,
    CustomerType CustomerType,
    string? FirstName,
    string? LastName,
    string? CompanyName,
    string PhoneNumber,
    string Identify,
    string Email,
    string Country,
    string Line1,
    string? Line2,
    string City,
    string State,
    string ZipCode,
    bool Active);
