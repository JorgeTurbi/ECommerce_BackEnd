using Store.Domain.Enums;

namespace Store.Application.Customers.GetAll;

/// <summary>
/// DTO de lectura resumido para listas de clientes.
/// </summary>
public sealed record CustomerListItemResponse(
    Guid Id,
    CustomerType CustomerType,
    string? FirstName,
    string? LastName,
    string? CompanyName,
    string DisplayName,
    string Email,
    string PhoneNumber,
    bool Active);
