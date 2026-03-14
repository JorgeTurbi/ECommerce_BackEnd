using ErrorOr;
using Store.Domain.Enums;
using Wolverine.Shims.MediatR;

namespace Store.Application.Customers.Create;

/// <summary>
/// Comando que representa la solicitud de creacion de un cliente.
/// </summary>
public sealed record CreateCustomerCommand(
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
    bool Active) : IRequest<ErrorOr<bool>>;
