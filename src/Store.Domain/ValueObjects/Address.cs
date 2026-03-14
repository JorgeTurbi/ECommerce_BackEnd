namespace Store.Domain.ValueObjects;

/// <summary>
/// Representa una direccion postal del cliente.
/// </summary>
public partial record Address
{
    /// <summary>
    /// Inicializa una nueva direccion con todos sus componentes.
    /// </summary>
    public Address(string country, string line1, string line2, string city, string state, string zipCode)
    {
        Country = country;
        Line1 = line1;
        Line2 = line2;
        City = city;
        State = state;
        ZipCode = zipCode;
    }

    /// <summary>
    /// Pais de la direccion.
    /// </summary>
    public string Country { get; init; }

    /// <summary>
    /// Primera linea de direccion.
    /// </summary>
    public string Line1 { get; init; }

    /// <summary>
    /// Segunda linea de direccion.
    /// </summary>
    public string Line2 { get; init; }

    /// <summary>
    /// Ciudad de la direccion.
    /// </summary>
    public string City { get; init; }

    /// <summary>
    /// Estado o provincia de la direccion.
    /// </summary>
    public string State { get; init; }

    /// <summary>
    /// Codigo postal de la direccion.
    /// </summary>
    public string ZipCode { get; init; }

    /// <summary>
    /// Fabrica la direccion a partir de sus componentes.
    /// </summary>
    public static Address Create(string country, string line1, string line2, string city, string state, string zipCode)
    {
        return new Address(country, line1, line2, city, state, zipCode);
    }
}
