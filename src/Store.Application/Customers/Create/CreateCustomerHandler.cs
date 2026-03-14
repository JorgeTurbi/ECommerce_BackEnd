using Store.Domain.Customers;
using Store.Domain.Primitives;
using Store.Domain.ValueObjects;

namespace Store.Application.Customers.Create;

/// <summary>
/// Maneja la creacion de nuevos clientes dentro de la aplicacion.
/// </summary>
public sealed class CreateCustomerHandler
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Inicializa el handler con las dependencias de escritura requeridas.
    /// </summary>
    public CreateCustomerHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    /// <summary>
    /// Ejecuta el caso de uso de creacion de cliente y confirma los cambios.
    /// </summary>
    public async Task<bool> Handle(CreateCustomerCommand command, CancellationToken ct)
    {
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
            new CustomerId(Guid.NewGuid()),
            command.Name,
            command.LastName,
            phoneNumber,
            identify,
            email,
            address,
            command.Active);

        await _customerRepository.AddAsync(customer);
        return await _unitOfWork.SaveChangesAsync(ct) > 0;
    }
}
