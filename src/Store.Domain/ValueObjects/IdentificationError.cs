using Store.Domain.Errors;

namespace Store.Domain.ValueObjects;

/// <summary>
/// Catalogo de errores asociados a la identificacion.
/// </summary>
public static class IdentificationError
{
    /// <summary>
    /// Error usado cuando la identificacion se recibe vacia.
    /// </summary>
    public static readonly Error Empty =
        new("identification.empty", "La identificacion no puede estar vacia.");

    /// <summary>
    /// Error usado cuando la identificacion contiene caracteres no permitidos.
    /// </summary>
    public static readonly Error InvalidCharacters =
        new("identification.invalid_characters", "La identificacion solo puede contener numeros.");

    /// <summary>
    /// Error usado cuando la longitud no coincide con una cedula o RNC validos.
    /// </summary>
    public static readonly Error InvalidLength =
        new("identification.invalid_length", "La identificacion debe tener 9 digitos (RNC) o 11 digitos (Cedula).");

    /// <summary>
    /// Error usado cuando la cedula no cumple el formato esperado.
    /// </summary>
    public static readonly Error InvalidCedula =
        new("identification.invalid_cedula", "La cedula no es valida.");

    /// <summary>
    /// Error usado cuando el RNC no cumple el formato esperado.
    /// </summary>
    public static readonly Error InvalidRnc =
        new("identification.invalid_rnc", "El RNC no es valido.");
}
