namespace Store.Domain.Customers;

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
    public Customer(CustomerId id, string name, string lastName, PhoneNumber phoneNumber, Identify identify, EmailAddress email, Address address, bool active)
    {
        Id = id;
        Name = name;
        LastName = lastName;
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

    /// <summary>
    /// Identificador unico del cliente.
    /// </summary>
    public CustomerId Id { get; private set; } = default!;

    /// <summary>
    /// Nombre del cliente.
    /// </summary>
    public string Name { get; private set; } = string.Empty;

    /// <summary>
    /// Apellido del cliente.
    /// </summary>
    public string LastName { get; private set; } = string.Empty;

    /// <summary>
    /// Nombre completo calculado a partir del nombre y apellido.
    /// </summary>
    public string FullName => $"{Name} {LastName}";

    /// <summary>
    /// Numero telefonico validado del cliente.
    /// </summary>
    public PhoneNumber PhoneNumber { get; private set; } = default!;

    /// <summary>
    /// Documento de identificacion del cliente.
    /// </summary>
    public Identify Identify { get; private set; } = default!;

    /// <summary>
    /// Correo electronico validado del cliente.
    /// </summary>
    public EmailAddress Email { get; private set; } = default!;

    /// <summary>
    /// Direccion principal del cliente.
    /// </summary>
    public Address Address { get; private set; } = default!;

    /// <summary>
    /// Indica si el cliente se encuentra activo en el sistema.
    /// </summary>
    public bool Active { get; private set; }
}
