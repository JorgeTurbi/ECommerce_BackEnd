namespace Store.Application.Customers.GetById;

/// <summary>
/// DTO de lectura detallado para un cliente.
/// </summary>
public sealed record CustomerDetailsResponse(
    Guid Id,
    string Name,
    string LastName,
    string FullName,
    string Email,
    string PhoneNumber,
    string Identify,
    string Country,
    string Line1,
    string Line2,
    string City,
    string State,
    string ZipCode,
    bool Active);
