using Microsoft.EntityFrameworkCore;
using Store.Application.Data;

namespace Store.Application.Customers.GetAll;

/// <summary>
/// Resuelve la consulta de listado de clientes.
/// </summary>
public sealed class GetAllCustomersHandler
{
    private readonly IApplicationDbContext _context;

    public GetAllCustomersHandler(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IReadOnlyList<CustomerListItemResponse>> Handle(GetAllCustomersQuery query, CancellationToken ct)
    {
        return await _context.Customers
            .AsNoTracking()
            .Select(customer => new CustomerListItemResponse(
                customer.Id.Value,
                customer.Type,
                customer.FirstName,
                customer.LastName,
                customer.CompanyName,
                customer.DisplayName,
                customer.Email.Value,
                customer.PhoneNumber.Value,
                customer.Active))
            .ToListAsync(ct);
    }
}
