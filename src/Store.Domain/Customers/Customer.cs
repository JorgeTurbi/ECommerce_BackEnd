namespace Store.Domain.Customers;

using Store.Domain.Enums;
using Store.Domain.Errors;
using Store.Domain.Primitives;
using Store.Domain.ValueObjects;

/// <summary>
/// Representa el agregado raiz de un cliente dentro del dominio.
/// </summary>
public sealed class Customer : AggregateRoot
{
    /// <summary>
    /// Inicializa una nueva instancia del cliente con todos sus datos de negocio.
    /// </summary>
    public Customer(
        CustomerId id,
        CustomerType customerType,
        string? firstName,
        string? lastName,
        string? companyName,
        PhoneNumber phoneNumber,
        Identify identify,
        EmailAddress email,
        Address address,
        bool active)
    {
        Validate(customerType, firstName, lastName, companyName, identify);

        Id = id;
        Type = customerType;
        FirstName = Normalize(firstName);
        LastName = Normalize(lastName);
        CompanyName = Normalize(companyName);
        PhoneNumber = phoneNumber;
        Identify = identify;
        Email = email;
        Address = address;
        Active = active;
    }

    /// <summary>
    /// Constructor requerido por algunas herramientas de persistencia.
    /// </summary>
    private Customer()
    {
    }

    public CustomerId Id { get; private set; } = default!;

    /// <summary>
    /// Tipo de cliente dentro del ecommerce.
    /// </summary>
    public CustomerType Type { get; private set; }

    /// <summary>
    /// Nombre del cliente cuando es una persona fisica.
    /// </summary>
    public string? FirstName { get; private set; }

    /// <summary>
    /// Apellido del cliente cuando es una persona fisica.
    /// </summary>
    public string? LastName { get; private set; }

    /// <summary>
    /// Nombre comercial o razon social cuando el cliente es una empresa.
    /// </summary>
    public string? CompanyName { get; private set; }

    /// <summary>
    /// Nombre visible del cliente para listados y pantallas.
    /// </summary>
    public string DisplayName =>
        Type == CustomerType.Business
            ? CompanyName ?? string.Empty
            : $"{FirstName} {LastName}".Trim();

    /// <summary>
    /// Alias de compatibilidad para vistas que aun esperan un nombre completo.
    /// </summary>
    public string FullName => DisplayName;

    public PhoneNumber PhoneNumber { get; private set; } = default!;

    public Identify Identify { get; private set; } = default!;

    public EmailAddress Email { get; private set; } = default!;

    public Address Address { get; private set; } = default!;

    public bool Active { get; private set; }

    private static void Validate(CustomerType customerType, string? firstName, string? lastName, string? companyName, Identify identify)
    {
        if (customerType == CustomerType.Individual)
        {
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
                throw new DomainException(new Error("customer.individual.name_required", "El cliente individual debe tener nombre y apellido."));

            if (identify.Type != IdentificationType.Cedula)
                throw new DomainException(new Error("customer.individual.identification_invalid", "Un cliente individual debe usar una cedula."));

            return;
        }

        if (string.IsNullOrWhiteSpace(companyName))
            throw new DomainException(new Error("customer.business.company_name_required", "El cliente empresa debe tener un nombre comercial o razon social."));

        if (identify.Type != IdentificationType.RNC)
            throw new DomainException(new Error("customer.business.identification_invalid", "Un cliente empresa debe usar un RNC."));
    }

    private static string? Normalize(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return null;

        return value.Trim();
    }
}
