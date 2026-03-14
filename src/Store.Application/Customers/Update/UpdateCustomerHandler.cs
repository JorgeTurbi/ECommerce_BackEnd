using Store.Domain.Customers;
using Store.Domain.Primitives;
using Store.Domain.ValueObjects;

namespace Store.Application.Customers.Update;

/// <summary>
/// Maneja la actualizacion de clientes existentes.
/// </summary>
public sealed class UpdateCustomerHandler
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Inicializa el handler con las dependencias de escritura requeridas.
    /// </summary>
    public UpdateCustomerHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    /// <summary>
    /// Actualiza un cliente existente y confirma los cambios.
    /// </summary>
    public async Task<bool> Handle(UpdateCustomerCommand command, CancellationToken ct)
    {
        var existingCustomer = await _customerRepository.GetByIdAsync(new CustomerId(command.CustomerId));

        if (existingCustomer is null)
        {
            return false;
        }

        if (PhoneNumber.Create(command.PhoneNumber) is not PhoneNumber phoneNumber)
        {
            throw new ArgumentException("Invalid phone number format.", nameof(command.PhoneNumber));
        }

        if (Identify.Create(command.Identify) is not Identify identify)
        {
            throw new ArgumentException("Invalid identify format.", nameof(command.Identify));
        }

        if (EmailAddress.Create(command.Email) is not EmailAddress email)
        {
            throw new ArgumentException("Invalid email format.", nameof(command.Email));
        }

        if (Address.Create(command.Country, command.Line1, command.Line2, command.City, command.State, command.ZipCode) is not Address address)
        {
            throw new ArgumentException("Invalid address format.", nameof(command.Line1));
        }

        var customer = new Customer(
            existingCustomer.Id,
            command.Name,
            command.LastName,
            phoneNumber,
            identify,
            email,
            address,
            command.Active);

        await _customerRepository.UpdateAsync(customer);
        return await _unitOfWork.SaveChangesAsync(ct) > 0;
    }
}
