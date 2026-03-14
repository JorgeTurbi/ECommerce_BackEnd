using Store.Domain.Errors;

namespace Store.Domain.ValueObjects;

/// <summary>
/// Catalogo de errores asociados al correo electronico.
/// </summary>
public static class EmailAddressErrors
{
    /// <summary>
    /// Error usado cuando el correo se recibe vacio.
    /// </summary>
    public static readonly Error Empty =
        new("email.empty", "El correo electronico no puede estar vacio.");

    /// <summary>
    /// Construye un error cuando el correo excede la longitud permitida.
    /// </summary>
    public static Error TooLong(int maxLength) =>
        new("email.too_long", $"El correo electronico no puede exceder {maxLength} caracteres.");

    /// <summary>
    /// Error usado cuando el formato del correo es invalido.
    /// </summary>
    public static readonly Error InvalidFormat =
        new("email.invalid_format", "El formato del correo electronico no es valido.");
}
