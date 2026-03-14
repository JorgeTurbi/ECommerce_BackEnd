using Microsoft.EntityFrameworkCore;
using Store.Application.Data;

namespace Store.Application.Customers.GetAll;

/// <summary>
/// Resuelve la consulta de listado de clientes.
/// </summary>
public sealed class GetAllCustomersHandler
{
    private readonly IApplicationDbContext _context;

    /// <summary>
    /// Inicializa el handler con el contexto de lectura.
    /// </summary>
    public GetAllCustomersHandler(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Obtiene todos los clientes proyectados a un modelo de lectura liviano.
    /// </summary>
    public async Task<IReadOnlyList<CustomerListItemResponse>> Handle(GetAllCustomersQuery query, CancellationToken ct)
    {
        return await _context.Customers
            .AsNoTracking()
            .Select(customer => new CustomerListItemResponse(
                customer.Id.Value,
                customer.Name,
                customer.LastName,
                customer.FullName,
                customer.Email.Value,
                customer.PhoneNumber.Value,
                customer.Active))
            .ToListAsync(ct);
    }
}
