using Microsoft.EntityFrameworkCore;
using Store.Application.Data;
using Store.Domain.Customers;

namespace Store.Application.Customers.GetById;

/// <summary>
/// Resuelve la consulta de detalle de un cliente.
/// </summary>
public sealed class GetCustomerByIdHandler
{
    private readonly IApplicationDbContext _context;

    public GetCustomerByIdHandler(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<CustomerDetailsResponse?> Handle(GetCustomerByIdQuery query, CancellationToken ct)
    {
        return await _context.Customers
            .AsNoTracking()
            .Where(customer => customer.Id == new CustomerId(query.CustomerId))
            .Select(customer => new CustomerDetailsResponse(
                customer.Id.Value,
                customer.Type,
                customer.FirstName,
                customer.LastName,
                customer.CompanyName,
                customer.DisplayName,
                customer.Email.Value,
                customer.PhoneNumber.Value,
                customer.Identify.Value,
                customer.Address.Country,
                customer.Address.Line1,
                customer.Address.Line2,
                customer.Address.City,
                customer.Address.State,
                customer.Address.ZipCode,
                customer.Active))
            .FirstOrDefaultAsync(ct);
    }
}
