using Store.Domain.Errors;

namespace Store.Domain.ValueObjects;



public static class EmailAddressErrors
{
    public static readonly Error Empty =
        new("email.empty", "El correo electrónico no puede estar vacío.");

    public static Error TooLong(int maxLength) =>
        new("email.too_long", $"El correo electrónico no puede exceder {maxLength} caracteres.");

    public static readonly Error InvalidFormat =
        new("email.invalid_format", "El formato del correo electrónico no es válido.");
}