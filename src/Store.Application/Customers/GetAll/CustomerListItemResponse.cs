namespace Store.Application.Customers.GetAll;

/// <summary>
/// DTO de lectura resumido para listas de clientes.
/// </summary>
public sealed record CustomerListItemResponse(
    Guid Id,
    string Name,
    string LastName,
    string FullName,
    string Email,
    string PhoneNumber,
    bool Active);
