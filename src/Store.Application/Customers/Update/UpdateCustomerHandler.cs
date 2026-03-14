using Store.Domain.Customers;
using Store.Domain.Errors;
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

    public UpdateCustomerHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<bool> Handle(UpdateCustomerCommand command, CancellationToken ct)
    {
        var existingCustomer = await _customerRepository.GetByIdAsync(new CustomerId(command.CustomerId));

        if (existingCustomer is null)
        {
            return false;
        }

        try
        {
            var phoneNumber = PhoneNumber.Create(command.PhoneNumber);
            var identify = Identify.Create(command.Identify);
            var email = EmailAddress.Create(command.Email);
            var address = Address.Create(command.Country, command.Line1, command.Line2 ?? string.Empty, command.City, command.State, command.ZipCode);

            var customer = new Customer(
                existingCustomer.Id,
                command.CustomerType,
                command.FirstName,
                command.LastName,
                command.CompanyName,
                phoneNumber,
                identify,
                email,
                address,
                command.Active);

            await _customerRepository.UpdateAsync(customer);
            return await _unitOfWork.SaveChangesAsync(ct) > 0;
        }
        catch (DomainException)
        {
            return false;
        }
    }
}
