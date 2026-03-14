using Store.Domain.Errors;

namespace Store.Domain.ValueObjects;

/// <summary>
/// Catalogo de errores asociados a numeros telefonicos.
/// </summary>
public static class PhoneError
{
    /// <summary>
    /// Error usado cuando el telefono se recibe vacio.
    /// </summary>
    public static readonly Error Empty =
        new("phone.empty", "El numero de telefono no puede estar vacio.");

    /// <summary>
    /// Construye un error cuando el telefono excede la longitud permitida.
    /// </summary>
    public static Error TooLong(int maxLength) =>
        new("phone.too_long", $"El numero de telefono no puede exceder {maxLength} caracteres.");

    /// <summary>
    /// Error usado cuando el formato del telefono es invalido.
    /// </summary>
    public static readonly Error InvalidFormat =
        new("phone.invalid_format", "El formato del numero de telefono no es valido.");
}
