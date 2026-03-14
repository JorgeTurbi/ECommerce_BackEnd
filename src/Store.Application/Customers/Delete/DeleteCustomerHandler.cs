using Store.Domain.Customers;
using Store.Domain.Primitives;

namespace Store.Application.Customers.Delete;

/// <summary>
/// Maneja la eliminacion de clientes.
/// </summary>
public sealed class DeleteCustomerHandler
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Inicializa el handler con las dependencias de escritura requeridas.
    /// </summary>
    public DeleteCustomerHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    /// <summary>
    /// Elimina un cliente si existe y confirma los cambios.
    /// </summary>
    public async Task<bool> Handle(DeleteCustomerCommand command, CancellationToken ct)
    {
        var customer = await _customerRepository.GetByIdAsync(new CustomerId(command.CustomerId));

        if (customer is null)
        {
            return false;
        }

        await _customerRepository.DeleteAsync(customer);
        return await _unitOfWork.SaveChangesAsync(ct) > 0;
    }
}
