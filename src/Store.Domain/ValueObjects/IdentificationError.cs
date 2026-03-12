
using Store.Domain.Errors;

namespace Store.Domain.ValueObjects;

public static class IdentificationError
{
    public static readonly Error Empty =
        new("identification.empty", "La identificación no puede estar vacía.");

    public static readonly Error InvalidCharacters =
        new("identification.invalid_characters", "La identificación solo puede contener números.");

    public static readonly Error InvalidLength =
        new("identification.invalid_length", "La identificación debe tener 9 dígitos (RNC) o 11 dígitos (Cédula).");

    public static readonly Error InvalidCedula =
        new("identification.invalid_cedula", "La cédula no es válida.");

    public static readonly Error InvalidRnc =
        new("identification.invalid_rnc", "El RNC no es válido.");
}