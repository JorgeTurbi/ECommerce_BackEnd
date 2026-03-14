using ErrorOr;
using Store.Domain.Customers;
using Store.Domain.Errors;
using Store.Domain.Primitives;
using Store.Domain.ValueObjects;
using Wolverine.Shims.MediatR;

namespace Store.Application.Customers.Create;

/// <summary>
/// Maneja la creacion de nuevos clientes dentro de la aplicacion.
/// </summary>
public sealed class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, ErrorOr<bool>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCustomerHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<bool>> Handle(CreateCustomerCommand command, CancellationToken ct)
    {
        try
        {
            var phoneNumber = PhoneNumber.Create(command.PhoneNumber);
            var identify = Identify.Create(command.Identify);
            var email = EmailAddress.Create(command.Email);
            var address = Address.Create(command.Country, command.Line1, command.Line2 ?? string.Empty, command.City, command.State, command.ZipCode);

            var customer = new Customer(
                new CustomerId(Guid.NewGuid()),
                command.CustomerType,
                command.FirstName,
                command.LastName,
                command.CompanyName,
                phoneNumber,
                identify,
                email,
                address,
                command.Active);

            await _customerRepository.AddAsync(customer);
            return await _unitOfWork.SaveChangesAsync(ct) > 0;
        }
        catch (DomainException ex)
        {
            return ErrorOr.Error.Validation(ex.Error.Code, ex.Error.Message);
        }
        catch (Exception ex)
        {
            return ErrorOr.Error.Failure("customer.create.unexpected", ex.Message);
        }
    }
}
