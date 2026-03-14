namespace Store.Domain.Customers;

/// <summary>
/// Define las operaciones de persistencia requeridas por el agregado <see cref="Customer"/>.
/// </summary>
public interface ICustomerRepository
{
    /// <summary>
    /// Agrega un nuevo cliente al almacenamiento.
    /// </summary>
    Task AddAsync(Customer customer);

    /// <summary>
    /// Obtiene un cliente por su identificador.
    /// </summary>
    Task<Customer?> GetByIdAsync(CustomerId id);

    /// <summary>
    /// Actualiza los datos persistidos de un cliente.
    /// </summary>
    Task UpdateAsync(Customer customer);

    /// <summary>
    /// Elimina un cliente del almacenamiento.
    /// </summary>
    Task DeleteAsync(Customer customer);
}
