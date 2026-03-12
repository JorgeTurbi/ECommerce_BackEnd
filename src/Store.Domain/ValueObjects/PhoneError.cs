

using Store.Domain.Errors;

namespace Store.Domain.ValueObjects;

public static class PhoneError
{
    public static readonly Error Empty =
        new("phone.empty", "El número de teléfono no puede estar vacío.");

    public static Error TooLong(int maxLength) =>
        new("phone.too_long", $"El número de teléfono no puede exceder {maxLength} caracteres.");

    public static readonly Error InvalidFormat =
        new("phone.invalid_format", "El formato del número de teléfono no es válido.");

}